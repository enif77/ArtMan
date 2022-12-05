/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Windows;

    using Injektor;
    using ArtMananager.DataObjects;


    /// <summary>
    /// Interaction logic for UmisteniEditor.xaml
    /// </summary>
    public partial class UmisteniEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Umisteni DataObject
        {
            get { return DataContext as Umisteni; }
        }

        #endregion


        public UmisteniEditor()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(Umisteni dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new UmisteniEditor
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "Art Manager - Nové umístění"
                    : String.Format("Art Manager - Úprava umístění {0}", dataObject.Id)
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
                ((Umisteni)DataContext).Validate();
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
    }
}
