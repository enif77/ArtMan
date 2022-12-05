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
                //_majitel1SingleSelect.ItemsSource = Gdo.UpdateAutorCollection();
                //_majitel2SingleSelect.ItemsSource = Gdo.MajitelCollection;
                _umisteniSingleSelect.ItemsSource = Gdo.UpdateUmisteniCollection();
            }
                               
            DialogResultState = DialogResultStateType.Ok;
        }


        #region public methods

        public static bool Open(GlobalDataObject gdo, Dilo dataObject)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");
            if (dataObject == null) throw new ArgumentNullException("dataObject");

            var dialog = new DiloEditor(gdo)
            {
                DataContext = dataObject,
                Owner = Registry.Get<MainWindow>(),
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = (dataObject.Id <= 0) 
                    ? "Nové dílo"
                    : String.Format("Úprava díla {0}", dataObject.Id)
            };

            dialog.LoadPreview();
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

        //private void ShowWiki_OnClick(object sender, RoutedEventArgs e)
        //{
        //    if (UIHelper.IsValidUrl(DataObject.WikipediaUrl))
        //    {
        //        System.Diagnostics.Process.Start(DataObject.WikipediaUrl);
        //    }
        //}


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
        

        private void AddAutor_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new Autor();
            if (AutorEditor.Open(Gdo, obj))
            {
                Gdo.SaveAutor(obj);
                Gdo.UpdateAutorCollection();
            }
        }

        private void AddTechnika_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new Technika();
            if (TechnikaEditor.Open(obj))
            {
                Gdo.SaveTechnika(obj);
                Gdo.UpdateTechnikaCollection();
            }
        }

        private void AddTypDila_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new TypDila();
            if (TypDilaEditor.Open(obj))
            {
                Gdo.SaveTypDila(obj);
                Gdo.UpdateTypDilaCollection();
            }
        }

        private void AddMena_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new Mena();
            if (MenaEditor.Open(obj))
            {
                Gdo.SaveMena(obj);
                Gdo.UpdateMenaCollection();
            }
        }

        private void AddProdejniMisto_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new ProdejniMisto();
            if (ProdejniMistoEditor.Open(obj))
            {
                Gdo.SaveProdejniMisto(obj);
                Gdo.UpdateProdejniMistoCollection();
            }
        }

        //private void AddMajitel_OnClick(object sender, RoutedEventArgs e)
        //{
        //    var obj = new Majitel();
        //    if (MajitelEditor.Open(obj))
        //    {
        //        Registry.Get<MajitelDataLayer>().Save(obj);

        //        Gdo.UpdateMajitelCollection();
        //    }
        //}

        private void AddUmisteni_OnClick(object sender, RoutedEventArgs e)
        {
            var obj = new Umisteni();
            if (UmisteniEditor.Open(obj))
            {
                Gdo.SaveUmisteni(obj);
                Gdo.UpdateUmisteniCollection();
            }
        }


        private void ProdejInfoButton_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: Mělo by se zamezit uložením změn při kliknutí na cancel (udělat klon a dílo aktualizovat jen při OK).

            ProdanoInformaceEditor.Open(this, Gdo, DataObject);
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
                _previewImage.Source = File.Exists(imgPath) ? new BitmapImage(new Uri(imgPath)) : new BitmapImage();

                var readmePath = Path.Combine(DataObject.ResourcesDir, "Readme.txt");
                _previewReadmeTextBox.Text = File.Exists(readmePath) ? File.ReadAllText(readmePath) : String.Empty;

                AdresareTreeView.ItemsSource = ItemProvider.GetItems(DataObject.ResourcesDir);
            }
        }
    }
}
