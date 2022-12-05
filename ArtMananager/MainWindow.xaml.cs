/* (C) 2016 - 2017 Premysl Fara */

namespace ArtMananager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Microsoft.Win32;

    using ArtMananager.Core;
    using ArtMananager.Datalayer;
    using ArtMananager.DataObjects;
    using ArtMananager.Forms;

    using Registry = Injektor.Registry;


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }
                   

        public DiloFilter Filter
        {
            get { return _gdo.Filter; }
        }


        private readonly List<string> _tempFilesList = new List<string>();


        public MainWindow()
        {
            _gdo = new GlobalDataObject();

            InitializeComponent();

            SourceInitialized += MainWindow_SourceInitiated;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;

            Registry.RegisterInstance(this);

            if (!UIHelper.DesignMode)
            {
                _autorSingleSelect.ItemsSource = Gdo.UpdateAutorCollection();
                _umisteniSingleSelect.ItemsSource = Gdo.UpdateUmisteniCollection();
                _koupenoKdeSingleSelect.ItemsSource = Gdo.UpdateProdejniMistoCollection();
                _majitelSingleSelect.ItemsSource = Gdo.UpdateMajitelCollection();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private void MainWindow_SourceInitiated(object sender, EventArgs e)
        {
            var config = App.Config;

            if (config.IsMaximized) WindowState = WindowState.Maximized;

            Top = config.Top;
            Left = config.Left;
            Width = config.Width;
            Height = config.Height;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            DiloDataGrid.Loaded += DataGrid_Loaded;

            DataContext = Filter;
            DiloDataGrid.ItemsSource = Gdo.DiloDataGridItems;

            Update();

            _initialized = true;
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //var config = (ApplicationConfiguration)ApplicationBase.Config;
            //if (config != null)
            //{
            //    if (_grid.GetGrid().ColumnInfo == null) _grid.GetGrid().UpdateColumnInfo();
            //    config.ReceivablesAndPayablesColumnInfo = _grid.GetGrid().ColumnInfo;
            //}

            // Vymazat seznam tempsouborů.
            foreach (var tempFilePath in _tempFilesList)
            {
                try
                {
                    File.Delete(tempFilePath);
                }
                catch (Exception)
                {
                    ; // Deleting of a temp file can fail.
                }
            }
        }


        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }


        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //var config = (ApplicationConfiguration)ApplicationBase.Config;
            //if (config != null && config.ReceivablesAndPayablesColumnInfo.Count > 0)
            //{
            //    _grid.GetGrid().ColumnInfo = config.ReceivablesAndPayablesColumnInfo;
            //}

            //InitContextMenu();
        }


        private void NewArtMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            AddDilo();
        }


        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddDilo();
        }


        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditDilo();
        }

        private void EditArtMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            EditDilo();
        }


        private void RefreshCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Update();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditDilo();
        }


        private void RefreshMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Update();
        }


        private void SaveDataToCsvMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ExportToCsv();
        }


        private void CopyDataToClipboardMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ExportToClipboard();
        }


        private void ShowArtPreviewsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var popup = new WaitPopupWindow() { Owner = Window.GetWindow(this) };

            DiloDataGrid.IsEnabled = false;
            popup.Show();

            try
            {
                ShowArtPreview(true);
            }
            finally 
            {
                popup.Close();
                DiloDataGrid.IsEnabled = true;
            }
        }
         
        private void ShowArtImagesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var popup = new WaitPopupWindow() { Owner = Window.GetWindow(this) };

            DiloDataGrid.IsEnabled = false;
            popup.Show();

            try
            {
                ShowArtPreview(false);
            }
            finally
            {
                popup.Close();
                DiloDataGrid.IsEnabled = true;
            }
        }


        private void ConfigurationMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (ConfigurationEditor.Open(App.Config))
            {
                Registry.Get<ConfigurationDataLayer>().Save(App.Config);
            }
        }


        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void KoupenoOdOnLostFocus(object sender, RoutedEventArgs e)
        {
            CheckYears();

            if (Filter.KoupenoOdRok > Filter.KoupenoDoRok)
            {
                Filter.KoupenoOdRok = Filter.KoupenoDoRok;
            }
        }

        private void KoupenoDoOnLostFocus(object sender, RoutedEventArgs e)
        {
            CheckYears();

            if (Filter.KoupenoDoRok < Filter.KoupenoOdRok)
            {
                Filter.KoupenoDoRok = Filter.KoupenoOdRok;
            }
        }


        //private void ProdanoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Gdo.Filter.Prodano = Int32.Parse(((ComboBoxItem) ProdanoComboBox.SelectedItem).Tag.ToString());
        //}


        public void Update()
        {
            this.InvokeIfRequired(() =>
            {
                PrepareFilter();

                Gdo.UpdateDilaCollection();
            }, DispatcherPriority.Render);
        }


        public void ClearFilter()
        {
            Filter.ClearAllExceptDates();
        }


        private void PrepareFilter()
        {
            this.InvokeIfRequired(() =>
            {
                CheckYears();

                // Clear multiselects
                //Filter.Clear();
            }, DispatcherPriority.Render);
        }

        private void CheckYears()
        {
            if (String.IsNullOrWhiteSpace(_koupenoOd.Text))
            {
                Filter.KoupenoOdRok = null;
            }

            if (String.IsNullOrWhiteSpace(_koupenoDo.Text))
            {
                Filter.KoupenoDoRok = null;
            }
        }


        private void AddDilo()
        {
            var obj = new Dilo();
            if (DiloEditor.Open(Gdo, obj))
            {
                Registry.Get<DiloDataLayer>().Save(obj);

                Update();
            }
        }


        private void EditDilo()
        {
            var item = DiloDataGrid.SelectedItem as DiloDataGridItem;
            if (item == null)
            {
                MessageBox.Show("Vyberte dílo k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);

                return;
            }

            // We Reload here to make sure, that we get, what is in the DB.   
            var entity = Registry.Get<DiloDataLayer>().Reload(item.Id);
            if (entity == null)
            {
                MessageBox.Show("Vybrané dílo nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            if (DiloEditor.Open(Gdo, entity))
            {
                Registry.Get<DiloDataLayer>().Save(entity);

                Update();
            }
        }


        private void DeleteDilo()
        {
            if (DiloDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybráno žádné dílo.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (
                MessageBox.Show(this, "Smazat označená díla?", "ArtMan - Smazání děl", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var dal = Registry.Get<DiloDataLayer>();
                foreach (var item in DiloDataGrid.SelectedItems)
                {
                    var entity = Registry.Get<DiloDataLayer>().Get((item as DiloDataGridItem).Id);
                    if (entity == null)
                    {
                        continue;
                    }

                    dal.Delete(entity);
                }

                Update();
            }
        }


        private void ExportToClipboard()
        {
            DataGridHelper.CopyToClipboard(DiloDataGrid);
        }


        private void ExportToCsv()
        {
            var dialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                OverwritePrompt = true,
                FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm"),
                DefaultExt = ".csv",
                Filter = "Comma-separated files (.csv)|*.csv"
            };

            // Operation canceled.
            if (dialog.ShowDialog().GetValueOrDefault() == false) return;

            // Convert data to CSV and store them to the clipboard.
            DataGridHelper.CopyToClipboard(DiloDataGrid);

            // Get the CSV data from the clipboard...
            var data = Clipboard.GetData(DataFormats.CommaSeparatedValue);

            try
            {
                using (var outfile = new StreamWriter(dialog.FileName))
                {
                    // ... and write the to the file.
                    outfile.Write(data.ToString());
                }
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);
            }
        }


        private void ShowArtPreview(bool thumbnailsOnly)
        {
            // získat seznam děl (varovat při prázdném a skončit)
            //   seznam obsahuje vše v zobrazené tabulce děl
            //   setříděno dle příjmení autorů

            var artsList = new Dictionary<string, List<ImageInfo>>();

            try
            {
                foreach (var item in Gdo.DiloDataGridItems)
                {
                    // Ignore authors without the surname.
                    if (String.IsNullOrWhiteSpace(item.AutorPrijmeni))
                    {
                        continue;
                    }

                    // Read the dilo to get the Resources dir.
                    var dilo = Registry.Get<DiloDataLayer>().Reload(item.Id);
                    if (dilo == null)
                    {
                        MessageBox.Show("Dílo s ID " + item.Id + " nelze načíst k zobrazení.", "ArtMan - Chyba",
                            MessageBoxButton.OK, MessageBoxImage.Error);

                        continue;
                    }

                    // Ignore arts without images.
                    if (String.IsNullOrWhiteSpace(dilo.ResourcesDir))
                    {
                        continue;
                    }

                    // Get images.
                    var diloImagesInfo = new List<ImageInfo>();
                    if (thumbnailsOnly)
                    {
                        var thumbnailPath = UIHelper.GetThumbnailImagePath(dilo.Id, dilo.ResourcesDir, dilo.ThumbnailName);
                        if (File.Exists(thumbnailPath))
                        {
                            var image = UIHelper.LoadImage(thumbnailPath);

                            diloImagesInfo.Add(new ImageInfo()
                            {
                                Path = thumbnailPath,
                                Width = image.Width,
                                Height = image.Height
                            });

                            // Remove the loaded image from the memory.
                            image = null;
                        }
                    }
                    else
                    {
                        var diloImages = ImagePreview.LoadImages(UIHelper.GetFullPath(dilo.ResourcesDir));
                        foreach (var image in diloImages)
                        {
                            diloImagesInfo.Add(new ImageInfo()
                            {
                                Path = image.Path,
                                Width = image.Image.Width,
                                Height = image.Image.Height
                            });

                            // Remove the loaded image from the memory.
                            image.Image = null;
                        }
                    }

                    // Ignore arts without loadable images.
                    if (diloImagesInfo.Count == 0)
                    {
                        continue;
                    }

                    // Add tis dilo images to the list of images per author.
                    if (artsList.ContainsKey(item.AutorPrijmeni) == false)
                    {
                        artsList.Add(item.AutorPrijmeni, new List<ImageInfo>());
                    }

                    artsList[item.AutorPrijmeni].AddRange(diloImagesInfo);
                }
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);

                return;
            }

            if (artsList.Count == 0)
            {
                MessageBox.Show(this, "Seznam děl je prázdný.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            // vygenerovat html string

            // The sorted list of authors.
            var authorsList = new List<string>();
            foreach (var authorName in artsList.Keys)
            {
                authorsList.Add(authorName);
            }

            authorsList.Sort();

            string previewSource;
            try
            {
                var previewSourceTemplate = PreviewTemplate.Load(@"Resources\PreviewTemplate.html");
                previewSource = previewSourceTemplate.GetSource(authorsList, artsList, App.Config.PreviewImageSize);
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);

                previewSource = PreviewTemplate.GetDefaultPreviewTemplate().GetSource(authorsList, artsList, App.Config.PreviewImageSize);
            }
             
            // vytvořit tempfile
            // vložit temp file do seznamu vytvořených temp souborů
            // vložit html string do tempfile
            // otevřít temfile v default applikaci

            try
            {
                var tempFile = Helpers.GetTempFileName(".html");
                _tempFilesList.Add(tempFile);
                
                File.WriteAllText(tempFile, previewSource);

                System.Diagnostics.Process.Start(tempFile);
            }
            catch (Exception ex)
            {
                UnhandledErrorWindow.Open(ex);
            }
        }
 

        private void BackupDataMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Backup files (*.zip)|*.zip|All files (*.*)|*.*",
                    CheckFileExists = false,
                    AddExtension = true,
                    Title = "Zvolte soubor zálohy",
                    InitialDirectory = "Backups",
                    FileName = "Zaloha_" + DateTime.Now.ToString("yyyyMMdd")
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    if (File.Exists(openFileDialog.FileName))
                    {
                        if (MessageBox.Show(this, "Přepsat zálohu?", "ArtMan - Přepsání zálohy", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                        {
                            return;
                        }

                        File.Delete(openFileDialog.FileName);
                    }

                    ZipFile.CreateFromDirectory(App.DataDirectoryPath, openFileDialog.FileName, CompressionLevel.Optimal, true);

                    MessageBox.Show("Záloha dat byla úspěšná.", "ArtMan - Informace", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Záloha dat byla zrušena.", "ArtMan - Informace", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Záloha dat se nepovedla: " + ex.Message, "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ManageLookupsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var lookupsWindow = new CiselnikyWindow(Gdo)
            {
                Owner = this,
                ShowInTaskbar = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            lookupsWindow.ShowDialog();
        }


        #region Buttons

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
             AddDilo();
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditDilo();
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteDilo();
        }

        #endregion
    }
}
