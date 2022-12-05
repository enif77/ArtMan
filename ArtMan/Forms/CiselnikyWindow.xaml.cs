/* (C) 2016 Premysl Fara */

namespace ArtMan.Forms
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using ArtMan.DataObjects;


    /// <summary>
    /// Interaction logic for CiselnikyWindow.xaml
    /// </summary>
    public partial class CiselnikyWindow : Window
    {
        private bool _initialized = false;
        private readonly GlobalDataObject _gdo;


        public GlobalDataObject Gdo
        {
            get { return _gdo; }
        }


        public CiselnikyWindow(GlobalDataObject gdo)
        {
            if (gdo == null) throw new ArgumentNullException("gdo");

            _gdo = gdo;
            
            InitializeComponent();

            Loaded += Window_Loaded;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_initialized) return;

            AutorDataGrid.ItemsSource = Gdo.AutorCollection;
            //KurzovniListekDataGrid.ItemsSource = Gdo.KurzovniListekCollection;
            MenaDataGrid.ItemsSource = Gdo.MenaCollection;       
            ProdejniMistoDataGrid.ItemsSource = Gdo.ProdejniMistoCollection;
            TechnikaDataGrid.ItemsSource = Gdo.TechnikaCollection;
            TypDilaDataGrid.ItemsSource = Gdo.TypDilaCollection;
            UmisteniDataGrid.ItemsSource = Gdo.UmisteniCollection;

            Update();

            _initialized = true;
        }


        private void Update()
        {
            if (AutorTabItem.IsSelected)
            {
                UpdateAutorCollection();
            }
            else if (MenaTabItem.IsSelected)
            {
                UpdateMenaCollection();
            }
            else if (ProdejniMistoTabItem.IsSelected)
            {
                UpdateProdejniMistoCollection();
            }
            else if (TechnikaTabItem.IsSelected)
            {
                UpdateTechnikaCollection();
            }
            else if (TypDilaTabItem.IsSelected)
            {
                UpdateTypDilaCollection();
            }
            else if (UmisteniTabItem.IsSelected)
            {
                UpdateUmisteniCollection();
            }
        }


        #region Buttons

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutorTabItem.IsSelected)
            {
                EditAutor(true);
            }
            else if (MenaTabItem.IsSelected)
            {
                EditMena(true);
            }
            else if (ProdejniMistoTabItem.IsSelected)
            {
                EditProdejniMisto(true);
            }
            else if (TechnikaTabItem.IsSelected)
            {
                EditTechnika(true);
            }
            else if (TypDilaTabItem.IsSelected)
            {
                EditTypDila(true);
            }
            else if (UmisteniTabItem.IsSelected)
            {
                EditUmisteni(true);
            }
        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutorTabItem.IsSelected)
            {
                EditAutor();
            }
            else if (MenaTabItem.IsSelected)
            {
                EditMena();
            }
            else if (ProdejniMistoTabItem.IsSelected)
            {
                EditProdejniMisto();
            }
            else if (TechnikaTabItem.IsSelected)
            {
                EditTechnika();
            }
            else if (TypDilaTabItem.IsSelected)
            {
                EditTypDila();
            }
            else if (UmisteniTabItem.IsSelected)
            {
                EditUmisteni();
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AutorTabItem.IsSelected)
            {
                DeleteAutor();
            }
            else if (MenaTabItem.IsSelected)
            {
                DeleteMena();
            }
            else if (ProdejniMistoTabItem.IsSelected)
            {
                DeleteProdejniMisto();
            }
            else if (TechnikaTabItem.IsSelected)
            {
                DeleteTechnika();
            }
            else if (TypDilaTabItem.IsSelected)
            {
                DeleteTypDila();
            }
            else if (UmisteniTabItem.IsSelected)
            {
                DeleteUmisteni();
            }
        }

        #endregion


        #region Autor
                       
        private void EditAutor(bool isNew = false)
        {
            Autor entity;

            if (isNew)
            {
                entity = new Autor();
            }
            else
            {
                var item = AutorDataGrid.SelectedItem as Autor;
                if (item == null)
                {
                    MessageBox.Show("Vyberte autora k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetAutor(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybraného autora nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }
            

            if (AutorEditor.Open(Gdo, entity))
            {
                Gdo.SaveAutor(entity);

                UpdateAutorCollection();
            }
        }


        private void DeleteAutor()
        {
            if (AutorDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrán žádný autor.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené autory?", "ArtMan - Smazání autorů", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in AutorDataGrid.SelectedItems)
                {
                    Gdo.DeleteAutor(item as Autor);
                }

                UpdateAutorCollection();
            }
        }


        private void UpdateAutorCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateAutorCollection();
            }, DispatcherPriority.Render);
        }


        private void AutorTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAutorCollection();
        }


        private void AutorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditAutor();
        }

        #endregion


        #region Mena

        private void EditMena(bool isNew = false)
        {
            Mena entity;

            if (isNew)
            {
                entity = new Mena();
            }
            else
            {
                var item = MenaDataGrid.SelectedItem as Mena;
                if (item == null)
                {
                    MessageBox.Show("Vyberte měnu k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetMena(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou měnu nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }

            if (MenaEditor.Open(entity))
            {
                Gdo.SaveMena(entity);

                UpdateMenaCollection();
            }
        }


        private void DeleteMena()
        {
            if (MenaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrána žádná měna.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené měny?", "ArtMan - Smazání měn", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in MenaDataGrid.SelectedItems)
                {
                    Gdo.DeleteMena(item as Mena);
                }

                UpdateMenaCollection();
            }
        }


        private void UpdateMenaCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateMenaCollection();
            }, DispatcherPriority.Render);
        }


        private void MenaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMenaCollection();
        }


        private void MenaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditMena();
        }

        #endregion


        #region ProdejniMisto

        private void EditProdejniMisto(bool isNew = false)
        {
            ProdejniMisto entity;

            if (isNew)
            {
                entity = new ProdejniMisto();
            }
            else
            {
                var item = ProdejniMistoDataGrid.SelectedItem as ProdejniMisto;
                if (item == null)
                {
                    MessageBox.Show("Vyberte prodejní místo k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetProdejniMisto(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybrané prodejní místo nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }
                
            if (ProdejniMistoEditor.Open(entity))
            {
                Gdo.SaveProdejniMisto(entity);

                UpdateProdejniMistoCollection();
            }
        }


        private void DeleteProdejniMisto()
        {
            if (ProdejniMistoDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybráno žádné prodejní místo.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označená prodejní místa?", "ArtMan - Smazání prodejních míst", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in ProdejniMistoDataGrid.SelectedItems)
                {
                    Gdo.DeleteProdejniMisto(item as ProdejniMisto);
                }

                UpdateProdejniMistoCollection();
            }
        }


        private void UpdateProdejniMistoCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateProdejniMistoCollection();
            }, DispatcherPriority.Render);
        }
                                 

        private void ProdejniMistoTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateProdejniMistoCollection(); 
        }


        private void ProdejniMistoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditProdejniMisto();
        }

        #endregion


        #region Technika

        private void EditTechnika(bool isNew = false)
        {
            Technika entity;

            if (isNew)
            {
                entity = new Technika();
            }
            else
            {
                var item = TechnikaDataGrid.SelectedItem as Technika;
                if (item == null)
                {
                    MessageBox.Show("Vyberte techniku k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetTechnika(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybranou techniku nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }

            if (TechnikaEditor.Open(entity))
            {
                Gdo.SaveTechnika(entity);

                UpdateTechnikaCollection();
            }
        }


        private void DeleteTechnika()
        {                  
            if (TechnikaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrána žádná technika.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené techniky?", "ArtMan - Smazání technik", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in TechnikaDataGrid.SelectedItems)
                {
                    Gdo.DeleteTechnika(item as Technika);
                }

                UpdateTechnikaCollection();
            }
        }


        private void UpdateTechnikaCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateTechnikaCollection();
            }, DispatcherPriority.Render);
        }


        private void TechnikaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTechnikaCollection();
        }


        private void TechnikaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditTechnika();
        }

        #endregion

         
        #region TypDila

        private void EditTypDila(bool isNew = false)
        {
            TypDila entity;

            if (isNew)
            {
                entity = new TypDila();
            }
            else
            {
                var item = TypDilaDataGrid.SelectedItem as TypDila;
                if (item == null)
                {
                    MessageBox.Show("Vyberte typ díla k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetTypDila(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybraný typ díla nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }
                
            if (TypDilaEditor.Open(entity))
            {
                Gdo.SaveTypDila(entity);

                UpdateTypDilaCollection();
            }
        }


        private void DeleteTypDila()
        {
            if (TypDilaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrán žádný typ díla.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené typy děl?", "ArtMan - Smazání typů děl", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in TypDilaDataGrid.SelectedItems)
                {
                    Gdo.DeleteTypDila(item as TypDila);
                }

                UpdateTypDilaCollection();
            }
        }


        private void UpdateTypDilaCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateTypDilaCollection();
            }, DispatcherPriority.Render);
        }


        private void TypDilaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTypDilaCollection();
        }


        private void TypDilaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditTypDila();
        }

        #endregion
        

        #region Umisteni

        private void EditUmisteni(bool isNew = false)
        {
            Umisteni entity;

            if (isNew)
            {
                entity = new Umisteni();
            }
            else
            {
                var item = UmisteniDataGrid.SelectedItem as Umisteni;
                if (item == null)
                {
                    MessageBox.Show("Vyberte umístění k editaci.", "ArtMan - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    return;
                }

                entity = Gdo.GetUmisteni(item.Id);
                if (entity == null)
                {
                    MessageBox.Show("Vybrané umístění nelze načíst k editaci.", "ArtMan - Chyba", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
            }
                 
            if (UmisteniEditor.Open(entity))
            {
                Gdo.SaveUmisteni(entity);

                UpdateUmisteniCollection();
            }
        }


        private void DeleteUmisteni()
        {
            if (UmisteniDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybráno žádné umístnění.", "ArtMan - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označená umístění?", "ArtMan - Smazání umístění", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in UmisteniDataGrid.SelectedItems)
                {
                    Gdo.DeleteUmisteni(item as Umisteni);
                }

                UpdateUmisteniCollection();
            }
        }


        private void UpdateUmisteniCollection()
        {
            this.InvokeIfRequired(() =>
            {
                Gdo.UpdateUmisteniCollection();
            }, DispatcherPriority.Render);
        }


        private void UmisteniTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUmisteniCollection();
        }


        private void UmisteniDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditUmisteni();
        }

        #endregion
    }
}
