/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;


    public class EzpCheckBox : CheckBox, IValueChanged
    {
        #region ctor

        static EzpCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(EzpCheckBox),
                new FrameworkPropertyMetadata(typeof(EzpCheckBox)));
        }


        public EzpCheckBox()
        {
            Checked += EzpCheckBox_ValueChanged;
            Unchecked += EzpCheckBox_ValueChanged;
        }

        #endregion


        #region CUSTOM EVENTS =================================================

        public event EventHandler SelectedValueChanged;

        #endregion


        #region Implementation of IValueChanged

        public static readonly DependencyProperty IsValueChangedProperty = DependencyProperty.Register("IsValueChanged", typeof(Boolean), typeof(EzpCheckBox));

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
                // Both current and original values are BO, compare them.
                if (this.OriginalValue is Boolean)
                {
                    this.IsValueChanged = this.IsChecked != (Boolean)this.OriginalValue;
                }
                else
                {
                    // Otherwise, we can not detect the change in the value.
                    this.IsValueChanged = false;
                }
            }

            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, EventArgs.Empty);
            }
        }

        #endregion


        #region event handlers

        private void EzpCheckBox_ValueChanged(object sender, RoutedEventArgs e)
        {
            CheckValueChange();
        }

        #endregion
    }
}
