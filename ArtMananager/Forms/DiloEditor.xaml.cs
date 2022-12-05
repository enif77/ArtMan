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
    /// Interaction logic for DiloEditor.xaml
    /// </summary>
    public partial class DiloEditor : Window
    {   
        #region properties

        public DialogResultStateType DialogResultState { get; private set; }


        public Dilo DataObject
        {
            get { return DataContext as Dilo; }
        }


        public GlobalDataObject Gdo { get; set; }

        #endregion


        public DiloEditor(GlobalDataObject gdo)
        {
            Gdo = gdo;

            InitializeComponent();

            if (!UIHelper.DesignMode)
            {
                _autorSingleSelect.ItemsSource = Gdo.UpdateAutorCollection();
                _technikaSingleSelect.ItemsSource = Gdo.UpdateTechnikaCollection();
                _typDilaSingleSelect.ItemsSource = Gdo.UpdateTypDilaCollection();
                _nakupMenaSingleSelect.ItemsSource = Gdo.UpdateMenaCollection();
                _koupenoKdeSingleSelect.ItemsSource = Gdo.UpdateProdejniMistoCollection();
                _majitelSingleSelect.ItemsSource = Gdo.UpdateMajitelCollection();
                _umisteniSingleSelect.ItemsSource = Gdo.UpdateUmisteniCollection();
            }
                               
            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(GlobalDataObject gdo, Dilo dataObject)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            // Ensure the relative resources dir path.
            dataObject.ResourcesDir = UIHelper.GetRelativePath(dataObject.ResourcesDir);

            var dialog = new DiloEditor(gdo)
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "Art Manager - Nové dílo"
                    : String.Format("Art Manager - Úprava díla {0}", dataObject.Id)
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


        private void DiloEditor_OnMouseDown(object sender, MouseButtonEventArgs e)
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
            try
            {
                ((Dilo)DataContext).Validate();
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


        private void EditAutor_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditAutor(this, Gdo, _autorSingleSelect.SelectedItem, false, false);
        }

        private void AddAutor_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditAutor(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.AutorId = entity.Id;
            }
        }


        private void EditTechnika_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditTechnika(this, Gdo, _technikaSingleSelect.SelectedItem, false, false);
        }

        private void AddTechnika_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditTechnika(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.TechnikaId = entity.Id;
            }
        }


        private void EditTypDila_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditTypDila(this, Gdo, _typDilaSingleSelect.SelectedItem, false, false);
        }

        private void AddTypDila_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditTypDila(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.TypDilaId = entity.Id;
            }
        }


        private void EditMena_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditMena(this, Gdo, _nakupMenaSingleSelect.SelectedItem, false, false);
        }

        private void AddMena_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditMena(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.NakupMenaId = entity.Id;
            }
        }


        private void EditProdejniMisto_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditProdejniMisto(this, Gdo, _koupenoKdeSingleSelect.SelectedItem, false, false);
        }

        private void AddProdejniMisto_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditProdejniMisto(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.KoupenoKdeId = entity.Id;
            }
        }


        private void EditMajitel_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditMajitel(this, Gdo, _majitelSingleSelect.SelectedItem, false, false);
        }

        private void AddMajitel_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditMajitel(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.MajitelId = entity.Id;
            }
        }


        private void SklademCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            if (DataObject.Kusu == 0)
            {
                DataObject.Kusu = 1;
            }
        }

        private void SklademCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataObject.Kusu = 0;
        }


        private void EditUmisteni_OnClick(object sender, RoutedEventArgs e)
        {
            UIHelper.EditUmisteni(this, Gdo, _umisteniSingleSelect.SelectedItem, false, false);
        }

        private void AddUmisteni_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = UIHelper.EditUmisteni(this, Gdo, null, true);
            if (entity != null)
            {
                DataObject.UmisteniId = entity.Id;
            }
        }


        //private void ProdejInfoButton_OnClick(object sender, RoutedEventArgs e)
        //{
        //    // TODO: Mělo by se zamezit uložením změn při kliknutí na cancel (udělat klon a dílo aktualizovat jen při OK).

        //    ProdanoInformaceEditor.Open(this, Gdo, DataObject);
        //}


        private void ImagesListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {                   
            _setThumbnailButton.IsEnabled = _imagesListView.SelectedItems.Count > 0;
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


        private void _nakupCena_OnLostFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _nakupCena.Visibility = Visibility.Hidden;

            // zviditelnit _nakupCenaView
            _nakupCenaView.Visibility = Visibility.Visible;
        }

        private void _nakupCenaView_OnGotFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _nakupCenaView.Visibility = Visibility.Hidden;

            // zviditelnit _nakupCena
            _nakupCena.Visibility = Visibility.Visible;

            // focus do _nakupCena
            _nakupCena.Focus();
        }


        private void _dnesniCena_OnLostFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _dnesniCena.Visibility = Visibility.Hidden;

            // zviditelnit _dnesniCenaView
            _dnesniCenaView.Visibility = Visibility.Visible;
        }

        private void _dnesniCenaView_OnGotFocus(object sender, RoutedEventArgs e)
        {
            // zneviditelnit
            _dnesniCenaView.Visibility = Visibility.Hidden;

            // zviditelnit _nakupCena
            _dnesniCena.Visibility = Visibility.Visible;

            // focus do _nakupCena
            _dnesniCena.Focus();
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
