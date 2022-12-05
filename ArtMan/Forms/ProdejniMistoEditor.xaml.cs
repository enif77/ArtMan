/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System;
    using System.Windows;

    using ArtMan.Core.Injektor;
    using ArtMan.DataObjects;


    /// <summary>
    /// Interaction logic for ProdejniMistoEditor.xaml
    /// </summary>
    public partial class ProdejniMistoEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public ProdejniMisto DataObject
        {
            get { return DataContext as ProdejniMisto; }
        }

        #endregion


        public ProdejniMistoEditor()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(ProdejniMisto dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new ProdejniMistoEditor
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "ArtMan - Nové prodejní místo"
                    : String.Format("ArtMan - Úprava prodejního místa {0}", dataObject.Id)
            };
               
            dialog.ShowDialog();

            return dialog.DialogResult.GetValueOrDefault();
        }

        #endregion


        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (SaveClick())
            {
                Close();
            }
        }


        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            CancelClick();
            Close();
        }


        private bool SaveClick()
        {
            try
            {
                ((ProdejniMisto)DataContext).Validate();
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);

                return false;
            }

            DialogResult = true;
            DialogResultState = DialogResultStateType.Ok;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = false;
            DialogResultState = DialogResultStateType.Cancel;
        }


        private void ShowWeb_OnClick(object sender, RoutedEventArgs e)
        {
            if (UIHelper.IsValidUrl(DataObject.WebUrl))
            {
                System.Diagnostics.Process.Start(DataObject.WebUrl);
            }
        }
    }
}
