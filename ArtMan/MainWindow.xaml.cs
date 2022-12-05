/* (C) 2016 Premysl Fara */

namespace ArtMan
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Microsoft.Win32;

    using ArtMan.Datalayer;
    using ArtMan.DataObjects;
    using ArtMan.Forms;

    using Registry = ArtMan.Core.Injektor.Registry;


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
            var config = (ApplicationConfiguration)App.Config;

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


        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void KoupenoOdOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Filter.KoupenoDoUi < Filter.KoupenoOdUi)
            {
                Filter.KoupenoDoUi = Filter.KoupenoOdUi;
            }
        }

        private void KoupenoDoOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Filter.KoupenoDoUi < Filter.KoupenoOdUi)
            {
                Filter.KoupenoOdUi = Filter.KoupenoDoUi;
            }
        }


        private void ProdanoOdOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Filter.ProdanoDoUi < Filter.ProdanoOdUi)
            {
                Filter.ProdanoDoUi = Filter.ProdanoOdUi;
            }
        }

        private void ProdanoDoOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (Filter.ProdanoDoUi < Filter.ProdanoOdUi)
            {
                Filter.ProdanoOdUi = Filter.ProdanoDoUi;
            }
        }


        private void SklademComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gdo.Filter.Skladem = Int32.Parse(((ComboBoxItem)SklademComboBox.SelectedItem).Tag.ToString());
        }


        private void ProdanoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gdo.Filter.Prodano = Int32.Parse(((ComboBoxItem)ProdanoComboBox.SelectedItem).Tag.ToString());
        }


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
                // Fix from-to date overlays.
                if (Filter.KoupenoDoUi < Filter.KoupenoOdUi)
                {
                    if (_koupenoDo.IsKeyboardFocusWithin)
                        Filter.KoupenoOdUi = Filter.KoupenoDoUi;
                    else
                        Filter.KoupenoDoUi = Filter.KoupenoOdUi;
                }

                // Get real from/to dates.
                Filter.KoupenoOd = (_koupenoOd.Value.HasValue)
                    ? _koupenoOd.Value.Value
                    : DateTime.MinValue;
                Filter.KoupenoDo = (_koupenoDo.Value.HasValue)
                    ? _koupenoDo.Value.Value
                    : DateTime.MaxValue;


                //// Fix from-to date overlays.
                //if (Filter.ProdanoDoUi < Filter.ProdanoOdUi)
                //{
                //    if (_prodanoDo.IsKeyboardFocusWithin)
                //        Filter.ProdanoOdUi = Filter.ProdanoDoUi;
                //    else
                //        Filter.ProdanoDoUi = Filter.ProdanoOdUi;
                //}

                //// Get real from/to dates.
                //Filter.ProdanoOd = (_prodanoOd.Value.HasValue)
                //    ? _prodanoOd.Value.Value
                //    : DateTime.MinValue;
                //Filter.ProdanoDo = (_prodanoDo.Value.HasValue)
                //    ? _prodanoDo.Value.Value
                //    : DateTime.MaxValue;

                // Clear multiselects
                //Filter.Clear();
            }, DispatcherPriority.Render);
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
                MessageBox.Show("Vyberte dílo k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return;
            }
                       
            var entity = Registry.Get<DiloDataLayer>().Get(item.Id);
            if (entity == null)
            {
                MessageBox.Show("Vybrané dílo nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

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

            if (MessageBox.Show(this, "Smazat označená díla?", "ArtMan - Smazání děl", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
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


        //public void LoadData(DiloFilter filter)
        //{
        //    var popup = new WaitPopupWindow() { Owner = Window.GetWindow(this) };

        //    _dataGrid.IsEnabled = false;
        //    popup.Show();

        //    _dataGrid.SelectedItem = null;
        //    _dataGrid.ItemsSource = Registry.Get<DiloDataLayer>().GetAll(filter);
        //    _dataGrid.Items.Refresh();

        //    popup.Close();
        //    _dataGrid.IsEnabled = true;
        //}

        ///// <summary>
        ///// Removes all data from the data grid.
        ///// </summary>
        //public void Clear()
        //{
        //    _dataGrid.SelectedItem = null;
        //    _dataGrid.ItemsSource = new object[0];
        //    _dataGrid.Items.Refresh();
        //}


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


        private void AddAutor_OnClick(object sender, RoutedEventArgs e)
        {
            AddDilo();
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
