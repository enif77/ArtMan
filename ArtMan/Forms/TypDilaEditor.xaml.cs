/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System;
    using System.Windows;

    using ArtMan.Core.Injektor;
    using ArtMan.DataObjects;


    /// <summary>
    /// Interaction logic for TypDilaEditor.xaml
    /// </summary>
    public partial class TypDilaEditor : Window
    {
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public TypDila DataObject
        {
            get { return DataContext as TypDila; }
        }

        #endregion


        public TypDilaEditor()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(TypDila dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new TypDilaEditor()
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "ArtMan - Nový typ díla" 
                    : String.Format("ArtMan - Úprava typu díla {0}", dataObject.Id)
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
                ((TypDila)DataContext).Validate();
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
