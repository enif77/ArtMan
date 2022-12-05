/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;


    /// <summary>
    /// Interaction logic for UnhandledErrorWindow.xaml
    /// </summary>
    public partial class UnhandledErrorWindow : Window
    {
        #region fields

        private static UnhandledErrorWindow _window;

        #endregion


        #region ctor

        public UnhandledErrorWindow()
        {
            InitializeComponent();
        }

        #endregion


        #region public methods

        public static void Open(Exception exception)
        {
            _window = new UnhandledErrorWindow
            {
                _errorTextBlock = {Text = exception.Message},
                _errorTextBox = {Text = exception.ToString()}
            };

            _window.ShowDialog();
        }

        #endregion


        #region non-public methods

        private void OkClick(object sender, RoutedEventArgs e)
        {
            _window.Close();
        }


        private void DetailsClick(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            _errorTextBox.Visibility = Visibility.Visible;
        }


        private void UIElement_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("mailto:artman@artman.cz");
        }

        #endregion
    }
}
