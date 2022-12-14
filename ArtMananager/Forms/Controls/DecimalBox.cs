/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms.Controls
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;


    public class DecimalBox : TextBox, IValueChanged, INotifyPropertyChanged
    {
        private string _oldValue;
        private int _oldSelectionLength;
        private int _oldSelectionStart;
        private Key _lastKey;

        /// <summary>
        /// Whether control treats an empty string as a null value (true) or a zero (false)
        /// </summary>
        public bool AllowEmpty { get; set; }

        /// <summary>
        /// Whether rewriting a zero with a zero again triggers a text change (true) or not (false)
        /// </summary>
        public bool ZeroAsChange { get; set; }
            
        /// <summary>
        /// The minimal value, that can be entered.
        /// </summary>
        public decimal? Minimum { get; set; }

        /// <summary>
        /// The maximal value, that can be entered.
        /// </summary>
        public decimal? Maximum { get; set; }

        static DecimalBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecimalBox), new FrameworkPropertyMetadata(typeof(DecimalBox)));
        }

        public DecimalBox()
        {
            HorizontalContentAlignment = HorizontalAlignment.Right;
        }

        #region CUSTOM EVENTS =================================================

        public event EventHandler SelectedValueChanged;

        #endregion


        #region Implementation of IValueChanged

        public static readonly DependencyProperty IsValueChangedProperty = DependencyProperty.Register("IsValueChanged", typeof(Boolean), typeof(DecimalBox));

        /// <summary>
        /// Returns true, if the current value differs from the original value.
        /// </summary>
        public bool IsValueChanged
        {
            get { return (bool)GetValue(IsValueChangedProperty); }
            set { SetValue(IsValueChangedProperty, value); }
        }


        /// <summary>
        /// Original value from DB/data source.
        /// </summary>
        public object OriginalValue
        {
            get { return this._originalValue; }

            set
            {
                _originalValue = value;

                // Remove the "dirty" color.
                IsValueChanged = false;
            }
        }

        private object _originalValue;


        /// <summary>
        /// Checks, if the currently selected value differs from the original value.
        /// </summary>
        private void CheckValueChange()
        {
            // No original value means, that value change detection is off.
            if (OriginalValue == null)
            {
                IsValueChanged = false;
            }
            else
            {
                // If nothing is selected, no change can be detected.
                // If something is selected, we can try to detect a change.
                IsValueChanged = Text != null && Text.Equals(OriginalValue) == false;
            }

            // Something happened, let's fire the event.
            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, EventArgs.Empty);
            }
        }

        #endregion


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoker for PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            TextChanged += TextBox_TextChanged;
            PreviewKeyDown += TextBox_KeyDown;
        }

        /// <summary>
        /// Clears all content from the textbox.
        /// </summary>
        public new void Clear()
        {
            Text  = AllowEmpty ? string.Empty : "0";
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            _oldSelectionLength = SelectionLength;
            _oldSelectionStart = SelectionStart;
            _lastKey = e.Key;
        }


        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Text.Contains(',') || Text.Contains('.'))
            {
                var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                TextChanged -= TextBox_TextChanged;
                Text = Text.Replace(",", decimalSeparator);
                Text = Text.Replace(".", decimalSeparator);
                TextChanged += TextBox_TextChanged;

                // Return caret position and selection after text replacement:
                var move = 0;
                if (_lastKey == Key.Back) move--;
                else if (_lastKey != Key.Delete) move++;  // Behind newly written character.

                SelectionStart = _oldSelectionStart + move;
                SelectionLength = _oldSelectionLength;
            }

            if (AllowEmpty)  // Replace all zeros for just one zero.
            {
                if (!string.IsNullOrEmpty(Text) &&
                    Text[0] == '0' &&
                    Text.Distinct().Count() == 1) // All zeros.
                    Text = "0";
            }
            else             // Replace all zeros or an empty string for just one zero.
            {
                if (string.IsNullOrEmpty(Text) || (Text[0] == '0' && Text.Distinct().Count() == 1)) // All zeros.

                    Text = "0";
            }

            if (IsEntryValid)
            {
                if (ZeroAsChange && _oldValue == "0" && Text == "0")
                {
                    // Trigger a text change:
                    if (Maximum != null && Maximum != 0)
                        Text = Maximum.ToString();
                    else if (Minimum != null && Minimum != 0)
                        Text = Minimum.ToString();
                    else
                        Text = "-1";

                    Text = "0";
                }
                _oldValue = Text;
            }
            else
            {
                Text = _oldValue;

                // Return caret position and selection after text replacement:
                SelectionStart = _oldSelectionStart;
                SelectionLength = _oldSelectionLength;
            }

            CheckValueChange();
        }

        /// <summary>
        /// Whether the current input can be edited further
        /// </summary>
        private bool IsEntryValid
        {
            get
            {
                if (AllowEmpty && string.IsNullOrEmpty(Text)) return true;

                var numberFormat = Thread.CurrentThread.CurrentCulture.NumberFormat;

                // Started to write a valid number that cannot be parsed yet:
                if (Text == numberFormat.NegativeSign || Text == numberFormat.NumberDecimalSeparator)
                {
                    return true;
                }

                // number with the sign at the end can be parsed, but is considered a bad input:
                int length = Text.Length;
                if (length > 1 && (Text.EndsWith(numberFormat.NegativeSign) || Text.EndsWith(numberFormat.PositiveSign)))
                {
                    return false;
                }

                decimal result;
                return decimal.TryParse(Text, out result) &&
                       (!Minimum.HasValue || result >= Minimum) &&
                       (!Maximum.HasValue || result <= Maximum);
            }
        }
    }
}
