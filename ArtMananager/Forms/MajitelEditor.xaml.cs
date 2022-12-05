/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Windows;

    using Injektor;
    using ArtMananager.DataObjects;


    /// <summary>
    /// Interaction logic for MajitelEditor.xaml
    /// </summary>
    public partial class MajitelEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Majitel DataObject
        {
            get { return DataContext as Majitel; }
        }

        #endregion


        public MajitelEditor()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(Majitel dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new MajitelEditor
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "Art Manager - Nový majitel"
                    : String.Format("Art Manager - Úprava majitele {0}", dataObject.Id)
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
                ((Majitel)DataContext).Validate();
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
