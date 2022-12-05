/* (C) 2016 Premysl Fara */

using System.Windows.Media;

namespace ArtMan.Forms.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;

    using Xceed.Wpf.Toolkit;

    using ArtMan.Core;


    /// <summary>
    /// DateTimePicker with ckeckbox indicating whether to use a selected or default value
    /// </summary>
    /// <remarks>
    /// Mandatory mode - always returns selected value (null if no value is selected)
    /// Minimum/Maximum mode - if checkbox is checked and value selected, returns that value, otherwise returns MinValue/MaxValue
    /// </remarks>
    [TemplatePart(Name = PartCheckBox, Type = typeof(CheckBox))]
    [TemplatePart(Name = PartDateTime, Type = typeof(DateTimePicker))]
    [ContentProperty("Value")]
    public class EzpDateTimePicker : Selector, IValueChanged
    {
        #region fields

        private const string PartCheckBox = "PART_CheckBox";
        private const string PartDateTime = "PART_DateTime";
        private CheckBox _chbUseSelected;
        private DateTimePicker _dateTimeSelect;

        public event RoutedPropertyChangedEventHandler<object> ValueChanged;

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(DateTime?), typeof(EzpDateTimePicker), new FrameworkPropertyMetadata(null, DateChanged));
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(EzpDatePickerMode), typeof(EzpDateTimePicker), new PropertyMetadata(EzpDatePickerMode.Mandatory));
        public static readonly DependencyProperty UseSelectedProperty = DependencyProperty.Register("UseSelected", typeof(bool), typeof(EzpDateTimePicker), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowUseSelectedCheckBoxProperty = DependencyProperty.Register("ShowUseSelectedCheckBox", typeof(bool), typeof(EzpDateTimePicker), new PropertyMetadata(false));
        public static readonly DependencyProperty TimePickerVisibilityProperty = DependencyProperty.Register("TimePickerVisibility", typeof(Visibility), typeof(EzpDateTimePicker), new UIPropertyMetadata(Visibility.Collapsed));  // time picker is not visible by default

        public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof(DateTimeFormat), typeof(EzpDateTimePicker));
        public static readonly DependencyProperty FormatStringProperty = DependencyProperty.Register("FormatString", typeof(string), typeof(EzpDateTimePicker));
        public static readonly DependencyProperty TimeFormatProperty = DependencyProperty.Register("TimeFormat", typeof(DateTimeFormat), typeof(EzpDateTimePicker));
        public static readonly DependencyProperty TimeFormatStringProperty = DependencyProperty.Register("TimeFormatString", typeof(string), typeof(EzpDateTimePicker));
        
        #endregion


        #region properties

        /// <summary>
        /// Whether mandatory checkbox is not visible (mandatory mode) or if it is,
        /// it will determine whether to use selected value (checked) or the default one (unchecked)
        /// </summary>
        /// <remarks>Default is Mandatory</remarks>
        public EzpDatePickerMode Mode
        {
            get { return (EzpDatePickerMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        /// <summary>
        /// Whether selection (true) or a default value (false) should be used
        /// </summary>
        /// <remarks>Unselected date will however cause return of a default value even if the checkbox is unchecked</remarks>
        public bool UseSelected
        {
            get
            {
                return (bool)GetValue(UseSelectedProperty);
            }
            set
            {
                SetValue(UseSelectedProperty, value);
            }
        }

        /// <summary>
        /// Whether the UseSelected checkbox is visible or not. It is set to false by default.
        /// </summary>
        public bool ShowUseSelectedCheckBox
        {
            get
            {
                return (bool)GetValue(ShowUseSelectedCheckBoxProperty);
            }
            set
            {
                SetValue(ShowUseSelectedCheckBoxProperty, value);
            }
        }

        /// <summary>
        /// DateTimePicker's value (or Min/Max Value based on <see cref="Mode"/> property)
        /// </summary>
        public DateTime? Value
        {
            get
            {
                var realValue = (DateTime?)GetValue(ValueProperty);
                bool hasValue = realValue.HasValue;
                switch (Mode)
                {
                    case EzpDatePickerMode.Minimum:
                        return (UseSelected && hasValue) ? realValue : Culture.DefaultTime;
                    case EzpDatePickerMode.Maximum:
                        return (UseSelected && hasValue) ? realValue : Culture.DefaultMaxTime;
                    default:
                        return (UseSelected && hasValue) ? realValue : DateTime.Today;
                }
            }

            set
            {
                SetValue(ValueProperty, value);
                if (_dateTimeSelect != null) _dateTimeSelect.Value = value;
            }
        }

        /// <summary>
        /// Visibility of TimePicker
        /// </summary>
        public Visibility TimePickerVisibility
        {
            get { return (Visibility)GetValue(TimePickerVisibilityProperty); }
            set { SetValue(TimePickerVisibilityProperty, value); }
        }

        /// <summary>
        /// Format type of the Datepicker
        /// </summary>
        public DateTimeFormat Format
        {
            get { return (DateTimeFormat)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        /// <summary>
        /// Custom format of the Datepicker
        /// </summary>
        public string FormatString
        {
            get { return (string)GetValue(FormatStringProperty); }
            set { SetValue(FormatStringProperty, value); }
        }

        /// <summary>
        /// Format of the time
        /// </summary>
        public DateTimeFormat TimeFormat
        {
            get { return (DateTimeFormat)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        /// <summary>
        /// Custom format of the time
        /// </summary>
        public string TimeFormatString
        {
            get { return (string)GetValue(TimeFormatStringProperty); }
            set { SetValue(TimeFormatStringProperty, value); }
        }

        #endregion


        #region ctor

        static EzpDateTimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EzpDateTimePicker), new FrameworkPropertyMetadata(typeof(EzpDateTimePicker)));
        }


        public EzpDateTimePicker()
        {
            IsTabStop = false;
        }

        #endregion


        #region custom events

        public event EventHandler SelectedValueChanged;

        #endregion


        #region public methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            // checkbox
            _chbUseSelected = (CheckBox)GetTemplateChild(PartCheckBox);
            _chbUseSelected.Click += UseSelectedClick;

            // datetimepicker
            _dateTimeSelect = (DateTimePicker)GetTemplateChild(PartDateTime);
            switch (Mode)  // update value that could be set by binding (i.e. before calling OnApplyTemplate)
            {
                case EzpDatePickerMode.Minimum:
                    if (Value != Culture.DefaultTime) _dateTimeSelect.Value = Value;
                    break;
                case EzpDatePickerMode.Maximum:
                    if (Value != Culture.DefaultMaxTime) _dateTimeSelect.Value = Value;
                    break;
                case EzpDatePickerMode.Mandatory:
                    if (Value == null) Value = DateTime.Now;  // if no value is selected, Now is the default starting value for Mandatory mode
                    _dateTimeSelect.Value = Value;
                    break;
            }

            // If user can not use the checkbox, make it checked.
            if (ShowUseSelectedCheckBox == false)
            {
                UseSelected = true;
            }

            _dateTimeSelect.ValueChanged += DateTimeValueChanged;
            _dateTimeSelect.IsEnabledChanged += DateTimePicker_OnIsEnabledChanged;

            // Set foregroung color.
            DateTimePicker_OnIsEnabledChanged(null, new DependencyPropertyChangedEventArgs());
        }

        #endregion


        #region Implementation of IValueChanged

        public static readonly DependencyProperty IsValueChangedProperty = DependencyProperty.Register("IsValueChanged", typeof(Boolean), typeof(EzpDateTimePicker));

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
            if (OriginalValue == null)
            {
                //if (Mode == EzpDatePickerMode.AllowEmpty)
                //    this.IsValueChanged = (this.Value != null);  // no original value with AllowEmpty -> change means new value is not null
                //else
                    IsValueChanged = false;  // no original value for other modes -> the value change detection is off
            }
            else
            {
                // Both current and original values are BO, compare them:
                IsValueChanged = OriginalValue.Equals(Value) == false;
            }

            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, EventArgs.Empty);
            }
        }

        #endregion


        #region non-public methods

        private void DateTimePicker_OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _dateTimeSelect.Foreground = _dateTimeSelect.IsEnabled ? Brushes.Black : Brushes.Gainsboro;
        }

        /// <summary>
        /// IsMandatory checkbox check
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void UseSelectedClick(object sender, RoutedEventArgs e)
        {
            UseSelected = _chbUseSelected.IsChecked.HasValue && _chbUseSelected.IsChecked.Value;

            if (ValueChanged != null) ValueChanged(sender, null);
        }

        /// <summary>
        /// DateTimePicker value change
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        void DateTimeValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((DateTime?)e.NewValue == Culture.DefaultTime)
            {
                Value = (DateTime?)e.OldValue ?? (Mode == EzpDatePickerMode.Maximum ? Culture.DefaultMaxTime : Culture.DefaultTime);
                e.Handled = true;
                return;
            }

            // if control does not have implicitly hidden TimePicker, close calendar immediately after a date selection is made
            // (because hidden TimePicker will not be edited):
            if (TimePickerVisibility != Visibility.Visible) _dateTimeSelect.IsOpen = false;

            if (ValueChanged != null) ValueChanged(sender, e);

            CheckValueChange();
        }


        private static void DateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (EzpDateTimePicker)d;
            control.Invalidate();
        }


        private void Invalidate()
        {
            // The _dateTimeSelect field is null when OnApplyTemplate() method was not called yet.
            if (_dateTimeSelect == null) return;

            switch (Mode)  // update value that could be set by binding (i.e. before calling OnApplyTemplate)
            {
                case EzpDatePickerMode.Minimum:
                    if (Value != Culture.DefaultTime) _dateTimeSelect.Value = Value;
                    break;
                case EzpDatePickerMode.Maximum:
                    if (Value != Culture.DefaultMaxTime) _dateTimeSelect.Value = Value;
                    break;
                case EzpDatePickerMode.Mandatory:
                    if (Value == null) Value = DateTime.Now;  // if no value is selected, Now is the default starting value for Mandatory mode
                    _dateTimeSelect.Value = Value;
                    break;
                //case EzpDatePickerMode.AllowEmpty:
                //    if (Value == Culture.DefaultTime) Value = null;  // if no value is selected, nothing is the default starting value for AllowEmpty mode
                //    _dateTimeSelect.Value = Value;
                //    break;
            }
        }

        #endregion
    }
}
