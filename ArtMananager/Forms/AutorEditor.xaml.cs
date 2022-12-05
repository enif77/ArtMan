/* (C) 2016 - 2017 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Injektor;

    using ArtMananager.Core;
    using ArtMananager.DataObjects;


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

            // Ensure the relative resources dir path.
            dataObject.ResourcesDir = UIHelper.GetRelativePath(dataObject.ResourcesDir);

            var dialog = new AutorEditor(gdo)
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "Art Manager - Nový autor"
                    : String.Format("Art Manager - Úprava autora {0}", dataObject.Id)
            };

            var popup = new WaitPopupWindow() { Owner = Registry.Get<MainWindow>() };
            popup.Show();

            try
            {
                dialog.LoadThumbnail();
                dialog.LoadPreview(UIHelper.GetFullPath(dataObject.ResourcesDir));
            }
            finally
            {
                popup.Close();
            }

            dialog.ShowDialog();

            return dialog.DialogResult.GetValueOrDefault();
        }

        #endregion

                                               
        private void AutorEditor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }


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
            var path = UIHelper.GetFullPath(DataObject.ResourcesDir);
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog() { SelectedPath = path })
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DataObject.ResourcesDir = UIHelper.GetRelativePath(dialog.SelectedPath);
                }
            }
        }


        private void ShowResourcesDir_OnClick(object sender, RoutedEventArgs e)
        {
            var path = UIHelper.GetFullPath(DataObject.ResourcesDir);
            if (UIHelper.IsPathValid(path))
            {
                System.Diagnostics.Process.Start(path);
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


        private void ImagesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _imagesButton.IsEnabled = _imagesListView.SelectedItems.Count > 0;
        }


        private void ImagesListView_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var image = ((ListViewItem)sender).Content as ImagePreview;
            if (image == null)
            {
                return;
            }

            Item.OpenFile(image.Path);
        }


        private void SetThumbnailButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                var imagePreview = _imagesListView.SelectedItem as ImagePreview;
                if (imagePreview != null)
                {
                    DataObject.ThumbnailName = Path.GetFileName(imagePreview.Path);
                    LoadThumbnail();
                }
            }, DispatcherPriority.Render);
        }


        private void RemoveThumbnailButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.InvokeIfRequired(() =>
            {
                // Set the image name to some crap (the empty string here will be replaced with the "Thumbnail.jpg").
                DataObject.ThumbnailName = "@";

                LoadThumbnail();
            }, DispatcherPriority.Render);
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
            this.InvokeIfRequired(() =>
            {
                LoadThumbnail();
                LoadPreview(UIHelper.GetFullPath(DataObject.ResourcesDir));
            }, DispatcherPriority.Render);
        }


        private void LoadPreview(string path)
        {
            // Images.
            _imagesListView.ItemsSource = ImagePreview.LoadImages(path);

            // Files.
            AdresareTreeView.ItemsSource = ItemProvider.GetItems(path);
        }


        private void LoadThumbnail()
        {
            UIHelper.LoadPreviewImage(DataObject, _previewImage, _previewName);
        }

    }
}
