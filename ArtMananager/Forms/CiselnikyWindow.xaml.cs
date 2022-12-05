/* (C) 2016 Premysl Fara */

namespace ArtMananager.Forms
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using ArtMananager.DataObjects;


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
            MajitelDataGrid.ItemsSource = Gdo.MajitelCollection;
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
                UIHelper.UpdateAutorCollection(this, Gdo);
            }
            else if (MajitelTabItem.IsSelected)
            {
                UIHelper.UpdateMajitelCollection(this, Gdo);
            }
            else if (MenaTabItem.IsSelected)
            {
                UIHelper.UpdateMenaCollection(this, Gdo);
            }
            else if (ProdejniMistoTabItem.IsSelected)
            {
                UIHelper.UpdateProdejniMistoCollection(this, Gdo);
            }
            else if (TechnikaTabItem.IsSelected)
            {
                UIHelper.UpdateTechnikaCollection(this, Gdo);
            }
            else if (TypDilaTabItem.IsSelected)
            {
                UIHelper.UpdateTypDilaCollection(this, Gdo);
            }
            else if (UmisteniTabItem.IsSelected)
            {
                UIHelper.UpdateUmisteniCollection(this, Gdo);
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
            else if (MajitelTabItem.IsSelected)
            {
                EditMajitel(true);
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
            else if (MajitelTabItem.IsSelected)
            {
                EditMajitel();
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
            else if (MajitelTabItem.IsSelected)
            {
                DeleteMajitel();
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
            UIHelper.EditAutor(this, Gdo, AutorDataGrid.SelectedItem, isNew);
        }


        private void DeleteAutor()
        {
            if (AutorDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrán žádný autor.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené autory?", "Art Manager - Smazání autorů", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in AutorDataGrid.SelectedItems)
                {
                    var autor = item as Autor;
                    if (autor == null || autor.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteAutor(autor);
                }

                UIHelper.UpdateAutorCollection(this, Gdo);
            }
        }


        private void AutorTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateAutorCollection(this, Gdo);
        }


        private void AutorDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditAutor();
        }

        #endregion


        #region Majitel

        private void EditMajitel(bool isNew = false)
        {
            UIHelper.EditMajitel(this, Gdo, MajitelDataGrid.SelectedItem, isNew);
        }


        private void DeleteMajitel()
        {
            if (MajitelDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrán žádný majitel.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené majitele?", "Art Manager - Smazání majitelů", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in MajitelDataGrid.SelectedItems)
                {
                    var majitel = item as Majitel;
                    if (majitel == null || majitel.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteMajitel(majitel);
                }

                UIHelper.UpdateMajitelCollection(this, Gdo);
            }
        }


        private void MajitelTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateMajitelCollection(this, Gdo);
        }


        private void MajitelDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditMajitel();
        }

        #endregion


        #region Mena

        private void EditMena(bool isNew = false)
        {
            UIHelper.EditMena(this, Gdo, MenaDataGrid.SelectedItem, isNew);
        }


        private void DeleteMena()
        {
            if (MenaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrána žádná měna.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené měny?", "Art Manager - Smazání měn", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in MenaDataGrid.SelectedItems)
                {
                    var mena = item as Mena;
                    if (mena == null || mena.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteMena(mena);
                }

                UIHelper.UpdateMenaCollection(this, Gdo);
            }
        }


        private void MenaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateMenaCollection(this, Gdo);
        }


        private void MenaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditMena();
        }

        #endregion


        #region ProdejniMisto

        private void EditProdejniMisto(bool isNew = false)
        {
            UIHelper.EditProdejniMisto(this, Gdo, ProdejniMistoDataGrid.SelectedItem, isNew);
        }


        private void DeleteProdejniMisto()
        {
            if (ProdejniMistoDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybráno žádné prodejní místo.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označená prodejní místa?", "Art Manager - Smazání prodejních míst", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in ProdejniMistoDataGrid.SelectedItems)
                {
                    var prodejniMisto = item as ProdejniMisto;
                    if (prodejniMisto == null || prodejniMisto.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteProdejniMisto(prodejniMisto);
                }

                UIHelper.UpdateProdejniMistoCollection(this, Gdo);
            }
        }
                                 

        private void ProdejniMistoTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateProdejniMistoCollection(this, Gdo);
        }


        private void ProdejniMistoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditProdejniMisto();
        }

        #endregion


        #region Technika

        private void EditTechnika(bool isNew = false)
        {
            UIHelper.EditTechnika(this, Gdo, TechnikaDataGrid.SelectedItem, isNew);
        }


        private void DeleteTechnika()
        {                  
            if (TechnikaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrána žádná technika.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené techniky?", "Art Manager - Smazání technik", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in TechnikaDataGrid.SelectedItems)
                {
                    var technika = item as Technika;
                    if (technika == null || technika.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteTechnika(technika);
                }

                UIHelper.UpdateTechnikaCollection(this, Gdo);
            }
        }


        private void TechnikaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateTechnikaCollection(this, Gdo);
        }


        private void TechnikaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditTechnika();
        }

        #endregion

         
        #region TypDila

        private void EditTypDila(bool isNew = false)
        {
            UIHelper.EditTypDila(this, Gdo, TypDilaDataGrid.SelectedItem, isNew);
        }


        private void DeleteTypDila()
        {
            if (TypDilaDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybrán žádný typ díla.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označené typy děl?", "Art Manager - Smazání typů děl", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in TypDilaDataGrid.SelectedItems)
                {
                    var typDila = item as TypDila;
                    if (typDila == null || typDila.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteTypDila(typDila);
                }

                UIHelper.UpdateTypDilaCollection(this, Gdo);
            }
        }


        private void TypDilaTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateTypDilaCollection(this, Gdo);
        }


        private void TypDilaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditTypDila();
        }

        #endregion
        

        #region Umisteni

        private void EditUmisteni(bool isNew = false)
        {
            UIHelper.EditUmisteni(this, Gdo, UmisteniDataGrid.SelectedItem, isNew);
        }


        private void DeleteUmisteni()
        {
            if (UmisteniDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "Není vybráno žádné umístnění.", "Art Manager - Upozornění", MessageBoxButton.OK);

                return;
            }

            if (MessageBox.Show(this, "Smazat označená umístění?", "Art Manager - Smazání umístění", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in UmisteniDataGrid.SelectedItems)
                {
                    var umisteni = item as Umisteni;
                    if (umisteni == null || umisteni.Id == 0)
                    {
                        MessageBox.Show("Tuto položku nelze smazat.", "Art Manager - Upozornění", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        continue;
                    }

                    Gdo.DeleteUmisteni(umisteni);
                }

                UIHelper.UpdateUmisteniCollection(this, Gdo);
            }
        }


        private void UmisteniTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UIHelper.UpdateUmisteniCollection(this, Gdo);
        }


        private void UmisteniDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditUmisteni();
        }

        #endregion
    }
}
