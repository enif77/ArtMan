/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using ArtMan.Core;
    using ArtMan.Core.Injektor;
    using ArtMan.DataObjects;


    /// <summary>
    /// Interaction logic for AutorEditor.xaml
    /// </summary>
    public partial class AutorEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Autor DataObject
        {
            get { return DataContext as Autor; }
        }


        public GlobalDataObject Gdo { get; set; }

        #endregion


        public AutorEditor(GlobalDataObject gdo)
        {
            Gdo = gdo;

            InitializeComponent();

            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(GlobalDataObject gdo, Autor dataObject)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new AutorEditor(gdo)
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "ArtMan - Nový autor"
                    : String.Format("ArtMan - Úprava autora {0}", dataObject.Id)
            };

            dialog.LoadPreview();

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
            DialogResult = true;
            DialogResultState = DialogResultStateType.Ok;

            return true;
        }


        private void CancelClick()
        {
            DialogResult = false;
            DialogResultState = DialogResultStateType.Cancel;
        }


        private void SetResourcesDir_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog() { SelectedPath = DataObject.ResourcesDir })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataObject.ResourcesDir = dialog.SelectedPath;
                }
            }
        }


        private void ShowResourcesDir_OnClick(object sender, RoutedEventArgs e)
        {
            if (UIHelper.IsPathValid(DataObject.ResourcesDir))
            {
                System.Diagnostics.Process.Start(DataObject.ResourcesDir);
            }
        }


        private void ShowWiki_OnClick(object sender, RoutedEventArgs e)
        {
            if (UIHelper.IsValidUrl(DataObject.WikipediaUrl))
            {
                System.Diagnostics.Process.Start(DataObject.WikipediaUrl);
            }
        }


        private void ShowWeb_OnClick(object sender, RoutedEventArgs e)
        {
            if (UIHelper.IsValidUrl(DataObject.WebUrl))
            {
                System.Diagnostics.Process.Start(DataObject.WebUrl);
            }
        }


        private void AdresareTreeView_OnItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as TreeViewItem;
            if (item == null) return;

            if (!item.IsSelected)
            {
                return;
            }

            Item.OpenFile((Item)item.DataContext);
        }


        private void RefreshPreviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadPreview();
        }


        private void LoadPreview()
        {
            if (UIHelper.IsPathValid(DataObject.ResourcesDir))
            {
                var imgPath = Path.Combine(DataObject.ResourcesDir, "Thumbnail.jpg");
                if (File.Exists(imgPath))
                {
                    _previewImage.Source = new BitmapImage(new Uri(imgPath));
                }

                var readmePath = Path.Combine(DataObject.ResourcesDir, "Readme.txt");
                if (File.Exists(readmePath))
                {
                    _previewReadmeTextBox.Text = File.ReadAllText(readmePath);
                }

                AdresareTreeView.ItemsSource = ItemProvider.GetItems(DataObject.ResourcesDir);
            }
        }
    }
}
