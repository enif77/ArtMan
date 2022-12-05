/* (C) 2016 - 2017 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Windows;

    using Injektor;

    using ArtMananager.DataObjects;


    /// <summary>
    /// Interaction logic for ConfigurationEditor.xaml
    /// </summary>
    public partial class ConfigurationEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Configuration DataObject
        {
            get { return DataContext as Configuration; }
        }

        #endregion


        public ConfigurationEditor()
        {
            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(Configuration dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new ConfigurationEditor
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
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
                ((Configuration) DataContext).Validate();
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


        private void ShowBaseResourcesDirectoryPath_OnClick(object sender, RoutedEventArgs e)
        {
            if (UIHelper.IsPathValid(DataObject.BaseResourcesDirectoryPath))
            {
                System.Diagnostics.Process.Start(DataObject.BaseResourcesDirectoryPath);
            }
        }

        private void SetBaseResourcesDirectoryPath_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog() { SelectedPath = DataObject.BaseResourcesDirectoryPath })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataObject.BaseResourcesDirectoryPath = dialog.SelectedPath;
                }
            }
        }
    }
}
