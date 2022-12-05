/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms.Controls
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;

    using Xceed.Wpf.Toolkit;

    using ArtMan.Core;


    public class DecimalBox : DecimalUpDown, IValueChanged, INotifyPropertyChanged
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

        static DecimalBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DecimalBox), new FrameworkPropertyMetadata(typeof(DecimalBox)));
        }

        public DecimalBox()
        {
            ValueChanged += DecimalBox_ValueChanged;
            ShowButtonSpinner = false;
        }

        void DecimalBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            CheckValueChange();
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
                this._originalValue = value;

                // Remove the "dirty" color.
                this.IsValueChanged = false;
            }
        }

        private object _originalValue;


        /// <summary>
        /// Checks, if the currently selected value differs from the original value.
        /// </summary>
        private void CheckValueChange()
        {
            // No original value means, that value change detection is off.
            if (this.OriginalValue == null)
            {
                this.IsValueChanged = false;
            }
            else
            {
                // If nothing is selected, no change can be detected.
                // If something is selected, we can try to detect a change.
                this.IsValueChanged = this.Value != null && this.Value.Equals(this.OriginalValue) == false;
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

            TextBox.TextChanged += TextBox_TextChanged;
            TextBox.PreviewKeyDown += TextBox_KeyDown;

            // Forcing our global CI.
            CultureInfo = Culture.GetCurrentApplicationCultureInfo();
        }

        /// <summary>
        /// Clears all content from the textbox.
        /// </summary>
        public void Clear()
        {
            Value = AllowEmpty ? (decimal?)null : 0;
            Text  = AllowEmpty ? string.Empty : "0";
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            _oldSelectionLength = TextBox.SelectionLength;
            _oldSelectionStart = TextBox.SelectionStart;
            _lastKey = e.Key;
        }


        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox.Text.Contains(',') || TextBox.Text.Contains('.'))
            {
                var decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                TextBox.TextChanged -= TextBox_TextChanged;
                TextBox.Text = TextBox.Text.Replace(",", decimalSeparator);
                TextBox.Text = TextBox.Text.Replace(".", decimalSeparator);
                TextBox.TextChanged += TextBox_TextChanged;

                // return caret position and selection after text replacement:
                int move = 0;
                if (_lastKey == Key.Back) move--;
                else if (_lastKey != Key.Delete) move++;  // behind newly written character
                TextBox.SelectionStart = _oldSelectionStart + move;
                TextBox.SelectionLength = _oldSelectionLength;
            }

            if (AllowEmpty)  // replace all zeros for just one zero
            {
                if (!string.IsNullOrEmpty(TextBox.Text) &&
                    TextBox.Text[0] == '0' &&
                    TextBox.Text.Distinct().Count() == 1) // all zeros
                    TextBox.Text = "0";
            }
            else             // replace all zeros or an empty string for just one zero
            {
                if (string.IsNullOrEmpty(TextBox.Text) ||
                    (TextBox.Text[0] == '0' && TextBox.Text.Distinct().Count() == 1)) // all zeros
                    TextBox.Text = "0";
            }

            if (IsEntryValid)
            {
                if (ZeroAsChange && _oldValue == "0" && TextBox.Text == "0")
                {
                    // trigger a text change:
                    if (Maximum != null && Maximum != 0)
                        TextBox.Text = Maximum.ToString();
                    else if (Minimum != null && Minimum != 0)
                        TextBox.Text = Minimum.ToString();
                    else
                        TextBox.Text = "-1";

                    TextBox.Text = "0";
                }
                _oldValue = TextBox.Text;
            }
            else
            {
                TextBox.Text = _oldValue;

                // return caret position and selection after text replacement:
                TextBox.SelectionStart = _oldSelectionStart;
                TextBox.SelectionLength = _oldSelectionLength;
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
                if (AllowEmpty && string.IsNullOrEmpty(TextBox.Text)) return true;

                var numberFormat = Thread.CurrentThread.CurrentCulture.NumberFormat;

                // started to write a valid number that cannot be parsed yet:
                if (TextBox.Text == numberFormat.NegativeSign || TextBox.Text == numberFormat.NumberDecimalSeparator)
                {
                    return true;
                }

                // number with the sign at the end can be parsed, but is considered a bad input:
                int length = TextBox.Text.Length;
                if (length > 1 && (TextBox.Text.EndsWith(numberFormat.NegativeSign) || TextBox.Text.EndsWith(numberFormat.PositiveSign)))
                {
                    return false;
                }

                decimal result;
                return decimal.TryParse(TextBox.Text, out result) &&
                       (!Minimum.HasValue || result >= Minimum) &&
                       (!Maximum.HasValue || result <= Maximum);
            }
        }
    }
}
