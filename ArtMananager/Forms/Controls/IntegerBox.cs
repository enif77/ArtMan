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


    public class IntegerBox : TextBox, IValueChanged, INotifyPropertyChanged
    {
        private string _oldValue;
        private int _oldSelectionLength;
        private int _oldSelectionStart;

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
        public int? Minimum { get; set; }

        /// <summary>
        /// The maximal value, that can be entered.
        /// </summary>
        public int? Maximum { get; set; }

        #region ctor

        static IntegerBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IntegerBox),
                new FrameworkPropertyMetadata(typeof(IntegerBox)));
        }


        public IntegerBox()
        {
            HorizontalContentAlignment = HorizontalAlignment.Right;
        }
        
        #endregion


        #region CUSTOM EVENTS =================================================

        public event EventHandler SelectedValueChanged;

        #endregion


        #region Implementation of IValueChanged

        public static readonly DependencyProperty IsValueChangedProperty = DependencyProperty.Register("IsValueChanged", typeof(Boolean), typeof(IntegerBox));

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
            get { return _originalValue; }

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
                // If something is selected (Value != null), we can try to detect a change.
                // If nothing is selected (Value == null), change is if something was selected before (OriginalValue != null).
                IsValueChanged = (Text != null) ? !Text.Equals(OriginalValue) : (OriginalValue != null);
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
            KeyDown += TextBox_KeyDown;
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
        }


        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (AllowEmpty)  // replace all zeros with just one zero
            {
                if (!string.IsNullOrEmpty(Text) &&
                    Text[0] == '0' &&
                    Text.Distinct().Count() == 1) // all zeros
                    Text = "0";
            }
            else             // replace all zeros or an empty string with just one zero
            {
                if (string.IsNullOrEmpty(Text) ||
                    (Text[0] == '0' && Text.Distinct().Count() == 1)) // all zeros
                    Text = "0";
            }

            if (IsEntryValid)
            {
                if (ZeroAsChange && _oldValue == "0" && Text == "0")
                {
                    Text = "-1";  // trigger a text change
                    Text = "0";
                }
                _oldValue = Text;
            }
            else
            {
                Text = _oldValue;

                // return caret position and selection after text replacement:
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

                // started to write a valid number that cannot be parsed yet:
                if (Text == numberFormat.NegativeSign || Text == numberFormat.PositiveSign)
                {
                    return true;
                }

                // number with the sign at the end can be parsed, but is considered a bad input:
                int length = Text.Length;
                if (length > 1 && (Text.EndsWith(numberFormat.NegativeSign) || Text.EndsWith(numberFormat.PositiveSign)))
                {
                    return false;
                }

                int result;
                return int.TryParse(Text, out result);
            }
        }
    }
}
