/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System.Windows;
    using System.Windows.Controls;


    public class WaitPopupWindow : Window
    {
        public WaitPopupWindow()
        {
            base.Height = 80;
            base.Width = 190;
            base.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            base.WindowStyle = WindowStyle.None;

            base.Content = new TextBlock
            {
                Text = "Čekejte prosím...",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }
    }
}
