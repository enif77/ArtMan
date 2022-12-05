/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;


    [TemplatePart(Name = PartTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PartListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PartPopup, Type = typeof(Popup))]
    [TemplatePart(Name = PartArrow, Type = typeof(Grid))]
    public class SingleSelect : Selector, IValueChanged, INotifyPropertyChanged
    {
        #region CONSTANTS =====================================================

        private const string PartTextBox = "PART_TextBox";
        private const string PartListBox = "PART_ListBox";
        private const string PartPopup = "PART_Popup";
        private const string PartArrow = "PART_Arrow";

        #endregion
        

        #region FIELDS ========================================================
        
        private TextBox _textBox;
        private ListBox _listBox;
        private Grid _arrow;
        private Popup _popup;

        private bool _suspendFiltering;

        public static readonly DependencyProperty DisabledOnDefaultProperty = DependencyProperty.Register("DisabledOnDefault", typeof(bool), typeof(SingleSelect));
        public static readonly DependencyProperty HasVoidedProperty = DependencyProperty.Register("HasVoided", typeof(bool), typeof(SingleSelect), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowVoidedProperty = DependencyProperty.Register("ShowVoided", typeof(bool), typeof(SingleSelect), new PropertyMetadata(false));

        #endregion


        #region PROPERTIES ====================================================

        /// <summary>
        /// Whether control is disabled when no data is loaded (true) or wheter it is always enabled (false)
        /// </summary>
        public bool DisabledOnDefault
        {
            get { return (bool)GetValue(DisabledOnDefaultProperty); }
            set { SetValue(DisabledOnDefaultProperty, value); }
        }
        
        private bool IsControlFocused
        {
            get { return _textBox.IsFocused || _listBox.IsFocused || _listBox.IsKeyboardFocusWithin || _popup.IsFocused || _arrow.IsFocused; }
        }


        public bool UseMoveToNextControl
        {
            get;
            set;
        }

        public bool HasVoided
        {
            get { return (bool)GetValue(HasVoidedProperty); }
            set { SetValue(HasVoidedProperty, value); }
        }

        public bool ShowVoided
        {
            get { return (bool)GetValue(ShowVoidedProperty); }
            set { SetValue(ShowVoidedProperty, value); }
        }

        #endregion


        #region IValueChanged =================================================

        public static readonly DependencyProperty IsValueChangedProperty = DependencyProperty.Register("IsValueChanged", typeof(Boolean), typeof(SingleSelect));

        /// <summary>
        /// Returns true, if the current value differs from the original value.
        /// </summary>
        public bool IsValueChanged
        {
            get { return (bool)GetValue(IsValueChangedProperty); }
            set { SetValue(IsValueChangedProperty, value); }
        }

        /// <summary>
        /// Displayed text
        /// </summary>
        public string Text
        {
            get { return GetText(SelectedItem); }
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
                this.IsValueChanged = this.SelectedItem != null && this.SelectedItem.Equals(this.OriginalValue) == false;
            }

            // TODO: Mělo by stačit this.SelectionChanged.
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



        #region CUSTOM EVENTS =================================================

        public event EventHandler OnMoveToNextControl;

        public event EventHandler SelectedValueChanged;

        #endregion


        #region CONSTRUCTORS ==================================================

        static SingleSelect()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SingleSelect), new FrameworkPropertyMetadata(typeof(SingleSelect)));
        }


        public SingleSelect()
        {
            DependencyPropertyDescriptor.FromProperty(SelectedItemProperty, typeof(SingleSelect)).AddValueChanged(this, SelectedItemChanged);
            IsTabStop = false;
            UseMoveToNextControl = true;

            LostKeyboardFocus += OnLostKeyboardFocus;
            LostFocus += OnLostFocus;
        }

        #endregion


        #region PUBLIC METHODS ================================================

        //private Brush _originalTextBoxBackground;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // textbox
            _textBox = (TextBox)GetTemplateChild(PartTextBox);
            _textBox.GotFocus += _textBox_GotFocus;
            _textBox.LostFocus += _textBox_LostFocus;
            _textBox.TextChanged += _textBox_TextChanged;
            _textBox.PreviewKeyDown += _textBox_PreviewKeyDown;
            _textBox.PreviewMouseDown += _textBox_PreviewMouseDown;
            _textBox.PreviewMouseUp += _textBox_PreviewMouseUp;

            SetSelectedItemText();

            // listbox
            _listBox = (ListBox)GetTemplateChild(PartListBox);
            _listBox.SelectionChanged += _listBox_SelectionChanged;
            _listBox.LostFocus += _listBox_LostFocus;
            _listBox.IsKeyboardFocusWithinChanged += _listBox_IsKeyboardFocusWithinChanged;
            _listBox.PreviewMouseDown += _listBox_PreviewMouseDown;
            
            // popup
            _popup = (Popup)GetTemplateChild(PartPopup);
            _popup.Opened += _popup_Opened;
            _popup.LostFocus += _popup_LostFocus;

            // arrow
            _arrow = (Grid)GetTemplateChild(PartArrow);
            _arrow.PreviewMouseDown += _arrow_PreviewMouseDown;

            Window window = Window.GetWindow(this);
            if (!UIHelper.DesignMode && window != null)
            {
                window.LocationChanged += window_LocationChanged;
                window.Deactivated += window_Deactivated;
            }

            if (DisabledOnDefault) IsEnabled = false;

            InitMenu();
        }

        #endregion


        #region NON-PUBLIC METHODS ============================================

        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            if (!DisabledOnDefault) return;  // this method is used for DisabledOnDefault=true only
            IsEnabled = newValue != null;
            base.OnItemsSourceChanged(oldValue, newValue);
        }

        private void ShowResults(bool filter)
        {
            var source = ItemsSource;
            if (filter && source != null && !String.IsNullOrEmpty(_textBox.Text))
            {
                var filtered = new List<object>();
                var filterText = _textBox.Text.ToUpperInvariant();
                foreach (var item in source)
                {
                    var text = GetText(item).ToUpperInvariant();
                    if (text.StartsWith(filterText)) filtered.Add(item);
                }

                foreach (object item in source)
                {
                    if (filtered.Contains(item)) continue;
                    var text = GetText(item).ToUpperInvariant();
                    if (text.Contains(filterText)) filtered.Add(item);
                }

                source = filtered.ToArray();
            }

            _listBox.ItemsSource = source;

            //var haveSelectedItem = source.Cast<object>().Any(item => item == SelectedItem);

            _listBox.SelectedItem = SelectedItem;
            //_listBox.ScrollIntoView(_listBox.SelectedItem);
            if (_listBox.Items.Count > 0) _listBox.ScrollIntoView(_listBox.Items[0]);
        }


        private void ClosePopup(object selectedItem)
        {
            if (selectedItem != null)
            {
                SelectedItem = selectedItem;

                if (_listBox.SelectedItem == null)
                {
                    _listBox.SelectedItem = selectedItem;
                }
            }

            _textBox.Text = GetText(SelectedItem);

            _popup.IsOpen = false;
        }


        private string GetText(object item)
        {
            if (item == null) return String.Empty;
            if (string.IsNullOrEmpty(DisplayMemberPath))
            {
                return (item.GetType().IsValueType || item.GetType() == typeof (string))
                    ? item.ToString() : String.Empty;
            }

            var property = item.GetType().GetProperty(DisplayMemberPath);
            if (property == null) throw new Exception("DisplayMemberPath is not valid");

            var value = property.GetValue(item, null);
            if (value == null) return String.Empty;

            return value.ToString();
        }


        private void SetSelectedItemText()
        {
            _suspendFiltering = true;

            _textBox.Text = GetText(SelectedItem);

            _suspendFiltering = false;
        }


        private void MoveToNextControl()
        {
            if (!UseMoveToNextControl) return;
            if (OnMoveToNextControl != null) OnMoveToNextControl(this, EventArgs.Empty);

            var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;
            if (keyboardFocus != null) keyboardFocus.MoveFocus(tRequest);
        }


        private void MoveToPreviousControl()
        {
            if (!UseMoveToNextControl) return;

            var tRequest = new TraversalRequest(FocusNavigationDirection.Previous);
            var keyboardFocus = Keyboard.FocusedElement as UIElement;
            if (keyboardFocus != null) keyboardFocus.MoveFocus(tRequest);
        }


        private void InitMenu()
        {
            if (!HasVoided) return;

            var menu = new ContextMenu();
            var showVoided = new MenuItem { Header = "Show Voided", IsCheckable = true };
            showVoided.Click += ShowVoidedClick;
            menu.Items.Add(showVoided);
            ContextMenu = menu;
        }

        void ShowVoidedClick(object sender, RoutedEventArgs e)
        {
            ShowVoided = !ShowVoided;
        }

        #endregion


        #region EVENT HANDLERS ================================================

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            _textBox.Focus();
            //_popup.IsOpen = true;
        }


        private void SelectedItemChanged(object sender, EventArgs e)
        {
            if (_textBox == null) return;

            SetSelectedItemText();

            // Check, if the currently selected value differs from the original value.
            CheckValueChange();
        }

        #endregion


        #region KEYBOARD EVENT HANDLERS =======================================

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab && Keyboard.IsKeyDown(Key.LeftShift))
            {
                MoveToPreviousControl();

                e.Handled = true;
                return;
            }

            if ((e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab) && _popup.IsOpen)
            {
                object selectedItem = (_listBox.Items.Count > 0) 
                    ? (_listBox.SelectedItem ?? _listBox.Items[0]) 
                    : _listBox.SelectedItem;

                ClosePopup(selectedItem);
                MoveToNextControl();

                e.Handled = true;
                return;
            }

            base.OnPreviewKeyDown(e);
        }


        private void _textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!_popup.IsOpen)
            {
                _popup.IsOpen = true;
                _textBox.Focus();
                _textBox.SelectAll();
            }

            if (e.Key == Key.Down)
            {
                int index = _listBox.SelectedIndex + 1 == _listBox.Items.Count ? 0 : _listBox.SelectedIndex + 1;
                _listBox.SelectedIndex = index;

                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                int index = _listBox.SelectedIndex - 1 < 0 ? _listBox.Items.Count - 1 : _listBox.SelectedIndex - 1;
                _listBox.SelectedIndex = index;

                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                ClosePopup(null);
            }
        }

        #endregion


        #region WINDOW EVENT HANDLERS =========================================

        /// <summary>
        /// Closes popup when window lost focus
        /// </summary>
        private void window_Deactivated(object sender, EventArgs e)
        {
            ClosePopup(null);
        }

        /// <summary>
        /// Fixes window moving
        /// </summary>
        private void window_LocationChanged(object sender, EventArgs e)
        {
            var offset = _popup.HorizontalOffset;
            _popup.HorizontalOffset = offset + 1;
            _popup.HorizontalOffset = offset;
        }
         

        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            ClosePopup(null);
        }


        private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            ClosePopup(null);
        }

        #endregion


        #region POPUP EVENT HANDLERS ==========================================

        private void _popup_Opened(object sender, EventArgs e)
        {
            ShowResults(false);
        }

        #endregion


        #region TEXTBOX EVENT HANDLERS ========================================

        private void _textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //_popup.IsOpen = true;
            _textBox.SelectAll();
        }


        private void _textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsControlFocused)
            {
                ClosePopup(null);
            }
        }


        private void _textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_suspendFiltering) return;

            ShowResults(true);
        }

        private void _textBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_popup.IsOpen)
            {
                ////_popup.IsOpen = true;

                _textBox.Focus();
                _textBox.SelectAll();

                e.Handled = true;
            }
        }

        private void _textBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_textBox.SelectionLength != _textBox.Text.Length)
            {
                _textBox.SelectAll();

                //e.Handled = true;
            }
        }
        
        #endregion


        #region LISTBOX EVENT HANDLERS ========================================

        private void _listBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsControlFocused) ClosePopup(null);
        }


        private void _listBox_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!IsControlFocused) ClosePopup(null);
        }


        private void _popup_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsControlFocused) ClosePopup(null);
        }


        private void _listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_listBox.SelectedItem != null)
            {
                SelectedItem = _listBox.SelectedItem;
                _listBox.ScrollIntoView(_listBox.SelectedItem);
                _textBox.CaretIndex = _textBox.Text.Length;
            }
        }


        private void _listBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = e.OriginalSource != null ? ContainerFromElement(_listBox, (DependencyObject)e.OriginalSource) as ListBoxItem : null;
            if (item != null)
            {
                ClosePopup(item.Content);
                MoveToNextControl();
                e.Handled = true;
            }
        }

        #endregion


        #region ARROW EVENT HANDLERS ==========================================
        
        void _arrow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_popup.IsOpen)
            {
                _popup.IsOpen = true;
                _textBox.Focus();
                _textBox.SelectAll();

                e.Handled = true;
            }
        }

        #endregion
    }


    #region SingleSelectTextBlock

    public class SingleSelectTextBlock : TextBlock
    {
        #region fields

        public static readonly DependencyProperty IsVoidedProperty = DependencyProperty.Register("IsVoided", typeof(bool), typeof(SingleSelectTextBlock), new PropertyMetadata(false));

        #endregion


        #region properties

        public bool IsVoided
        {
            get { return (bool)GetValue(IsVoidedProperty); }
            set { SetValue(IsVoidedProperty, value); }
        }

        #endregion
    }

    #endregion
}
