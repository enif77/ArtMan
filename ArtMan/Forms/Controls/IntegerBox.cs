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

    using Core;


    public class IntegerBox : IntegerUpDown, IValueChanged, INotifyPropertyChanged
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


        #region ctor

        static IntegerBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(IntegerBox),
                new FrameworkPropertyMetadata(typeof(IntegerBox)));
        }


        public IntegerBox()
        {
            ShowButtonSpinner = false;
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
                IsValueChanged = (Value != null) ? !Value.Equals(OriginalValue) : (OriginalValue != null);
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
            TextBox.KeyDown += TextBox_KeyDown;

            // Forcing our global CI.
            CultureInfo = Culture.GetCurrentApplicationCultureInfo();
        }

        /// <summary>
        /// Clears all content from the textbox.
        /// </summary>
        public void Clear()
        {
            Value = AllowEmpty ? (int?)null : 0;
            Text  = AllowEmpty ? string.Empty : "0";
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            _oldSelectionLength = TextBox.SelectionLength;
            _oldSelectionStart = TextBox.SelectionStart;
        }


        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (AllowEmpty)  // replace all zeros with just one zero
            {
                if (!string.IsNullOrEmpty(TextBox.Text) &&
                    TextBox.Text[0] == '0' &&
                    TextBox.Text.Distinct().Count() == 1) // all zeros
                    TextBox.Text = "0";
            }
            else             // replace all zeros or an empty string with just one zero
            {
                if (string.IsNullOrEmpty(TextBox.Text) ||
                    (TextBox.Text[0] == '0' && TextBox.Text.Distinct().Count() == 1)) // all zeros
                    TextBox.Text = "0";
            }

            if (IsEntryValid)
            {
                if (ZeroAsChange && _oldValue == "0" && TextBox.Text == "0")
                {
                    TextBox.Text = "-1";  // trigger a text change
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
                if (TextBox.Text == numberFormat.NegativeSign || TextBox.Text == numberFormat.PositiveSign)
                {
                    return true;
                }

                // number with the sign at the end can be parsed, but is considered a bad input:
                int length = TextBox.Text.Length;
                if (length > 1 && (TextBox.Text.EndsWith(numberFormat.NegativeSign) || TextBox.Text.EndsWith(numberFormat.PositiveSign)))
                {
                    return false;
                }

                int result;
                return int.TryParse(TextBox.Text, out result);
            }
        }
    }
}
