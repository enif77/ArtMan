/* (C) 2016 Premysl Fara */

namespace ArtMananager.DataObjects
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using Injektor;

    using ArtMananager.Core.Collections;
    using ArtMananager.Datalayer;

    
    public class GlobalDataObject : 
        ICollectionSynchronizer<DiloDataGridItem>, 
        ICollectionSynchronizer<Autor>,
        ICollectionSynchronizer<Majitel>,
        ICollectionSynchronizer<Mena>,
        ICollectionSynchronizer<ProdejniMisto>,
        ICollectionSynchronizer<Technika>,
        ICollectionSynchronizer<TypDila>,
        ICollectionSynchronizer<Umisteni>,
        INotifyPropertyChanged
    {
        private readonly DiloFilter _filter = new DiloFilter();
        private readonly ExtendedObservableCollection<DiloDataGridItem> _itemsCollection;
        private readonly ExtendedObservableCollection<Autor> _autorCollection;
        private readonly ExtendedObservableCollection<Majitel> _majitelCollection;
        private readonly ExtendedObservableCollection<Mena> _menaCollection;
        private readonly ExtendedObservableCollection<ProdejniMisto> _prodejniMistoCollection;
        private readonly ExtendedObservableCollection<Technika> _technikaCollection;
        private readonly ExtendedObservableCollection<TypDila> _typDilaCollection;
        private readonly ExtendedObservableCollection<Umisteni> _umisteniCollection;


        #region Properties

        public DiloFilter Filter
        {
            get { return _filter; }
        }


        public ExtendedObservableCollection<DiloDataGridItem> DiloDataGridItems
        {
            get { return _itemsCollection; }
        }


        public ExtendedObservableCollection<Autor> AutorCollection
        {
            get { return _autorCollection; }
        }


        public ExtendedObservableCollection<Majitel> MajitelCollection
        {
            get { return _majitelCollection; }
        }


        public ExtendedObservableCollection<Mena> MenaCollection
        {
            get { return _menaCollection; }
        }


        public ExtendedObservableCollection<ProdejniMisto> ProdejniMistoCollection
        {
            get { return _prodejniMistoCollection; }
        }


        public ExtendedObservableCollection<Technika> TechnikaCollection
        {
            get { return _technikaCollection; }
        }


        public ExtendedObservableCollection<TypDila> TypDilaCollection
        {
            get { return _typDilaCollection; }
        }


        public ExtendedObservableCollection<Umisteni> UmisteniCollection
        {
            get { return _umisteniCollection; }
        }

        #endregion


        #region ctor

        public GlobalDataObject()
        {
            _itemsCollection = new ExtendedObservableCollection<DiloDataGridItem>();
            _autorCollection = new ExtendedObservableCollection<Autor>();
            _majitelCollection = new ExtendedObservableCollection<Majitel>();
            _menaCollection = new ExtendedObservableCollection<Mena>();
            _prodejniMistoCollection = new ExtendedObservableCollection<ProdejniMisto>();
            _technikaCollection = new ExtendedObservableCollection<Technika>();
            _typDilaCollection = new ExtendedObservableCollection<TypDila>();
            _umisteniCollection = new ExtendedObservableCollection<Umisteni>();
        }

        #endregion


        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Invoke property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region Collection Updaters

        public ExtendedObservableCollection<DiloDataGridItem> UpdateDilaCollection()
        {
            _itemsCollection.SynchronizeWith(new Collection<DiloDataGridItem>(Registry.Get<DiloDataLayer>().GetAll(Filter)), this);

            return DiloDataGridItems;
        }

        
        public ExtendedObservableCollection<Autor> UpdateAutorCollection()
        {
            _autorCollection.SynchronizeWith(new Collection<Autor>(Registry.Get<AutorDataLayer>().GetAll().ToList()), this);

            return AutorCollection;
        }


        public ExtendedObservableCollection<Majitel> UpdateMajitelCollection()
        {
            _majitelCollection.SynchronizeWith(new Collection<Majitel>(Registry.Get<MajitelDataLayer>().GetAll().ToList()), this);

            return MajitelCollection;
        }


        public ExtendedObservableCollection<Mena> UpdateMenaCollection()
        {
            _menaCollection.SynchronizeWith(new Collection<Mena>(Registry.Get<MenaDataLayer>().GetAll().ToList()), this);

            return MenaCollection;
        }
                     

        public ExtendedObservableCollection<ProdejniMisto> UpdateProdejniMistoCollection()
        {
            _prodejniMistoCollection.SynchronizeWith(new Collection<ProdejniMisto>(Registry.Get<ProdejniMistoDataLayer>().GetAll().ToList()), this);

            return ProdejniMistoCollection;
        }


        public ExtendedObservableCollection<Technika> UpdateTechnikaCollection()
        {
            _technikaCollection.SynchronizeWith(new Collection<Technika>(Registry.Get<TechnikaDataLayer>().GetAll().ToList()), this);

            return TechnikaCollection;
        }


        public ExtendedObservableCollection<TypDila> UpdateTypDilaCollection()
        {
            _typDilaCollection.SynchronizeWith(new Collection<TypDila>(Registry.Get<TypDilaDataLayer>().GetAll().ToList()), this);

            return TypDilaCollection;
        }


        public ExtendedObservableCollection<Umisteni> UpdateUmisteniCollection()
        {
            _umisteniCollection.SynchronizeWith(new Collection<Umisteni>(Registry.Get<UmisteniDataLayer>().GetAll().ToList()), this);

            return UmisteniCollection;
        }

        #endregion


        #region Data Operators

        public Autor GetAutor(int id)
        {
            return Registry.Get<AutorDataLayer>().Get(id);
        }

        public void SaveAutor(Autor obj)
        {
            Registry.Get<AutorDataLayer>().Save(obj);
        }

        public void DeleteAutor(Autor obj)
        {
            Registry.Get<AutorDataLayer>().Delete(obj);
        }


        public Majitel GetMajitel(int id)
        {
            return Registry.Get<MajitelDataLayer>().Get(id);
        }

        public void SaveMajitel(Majitel obj)
        {
            Registry.Get<MajitelDataLayer>().Save(obj);
        }

        public void DeleteMajitel(Majitel obj)
        {
            Registry.Get<MajitelDataLayer>().Delete(obj);
        }


        public Mena GetMena(int id)
        {
            return Registry.Get<MenaDataLayer>().Get(id);
        }

        public void SaveMena(Mena obj)
        {
            Registry.Get<MenaDataLayer>().Save(obj);
        }

        public void DeleteMena(Mena obj)
        {
            Registry.Get<MenaDataLayer>().Delete(obj);
        }


        public ProdejniMisto GetProdejniMisto(int id)
        {
            return Registry.Get<ProdejniMistoDataLayer>().Get(id);
        }

        public void SaveProdejniMisto(ProdejniMisto obj)
        {
            Registry.Get<ProdejniMistoDataLayer>().Save(obj);
        }

        public void DeleteProdejniMisto(ProdejniMisto obj)
        {
            Registry.Get<ProdejniMistoDataLayer>().Delete(obj);
        }


        public Technika GetTechnika(int id)
        {
            return Registry.Get<TechnikaDataLayer>().Get(id);
        }

        public void SaveTechnika(Technika obj)
        {
            Registry.Get<TechnikaDataLayer>().Save(obj);
        }

        public void DeleteTechnika(Technika obj)    
        {
            Registry.Get<TechnikaDataLayer>().Delete(obj);
        }


        public TypDila GetTypDila(int id)
        {
            return Registry.Get<TypDilaDataLayer>().Get(id);
        }

        public void SaveTypDila(TypDila obj)
        {
            Registry.Get<TypDilaDataLayer>().Save(obj);
        }

        public void DeleteTypDila(TypDila obj)
        {
            Registry.Get<TypDilaDataLayer>().Delete(obj);
        }


        public Umisteni GetUmisteni(int id)
        {
            return Registry.Get<UmisteniDataLayer>().Get(id);
        }

        public void SaveUmisteni(Umisteni obj)
        {
            Registry.Get<UmisteniDataLayer>().Save(obj);
        }

        public void DeleteUmisteni(Umisteni obj)
        {
            Registry.Get<UmisteniDataLayer>().Delete(obj);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<DiloDataGridItem>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(DiloDataGridItem a, DiloDataGridItem b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(DiloDataGridItem a, DiloDataGridItem b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<Autor>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(Autor a, Autor b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(Autor a, Autor b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<Majitel>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(Majitel a, Majitel b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(Majitel a, Majitel b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<Mena>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(Mena a, Mena b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(Mena a, Mena b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<ProdejniMisto>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(ProdejniMisto a, ProdejniMisto b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(ProdejniMisto a, ProdejniMisto b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<Technika>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(Technika a, Technika b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(Technika a, Technika b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<TypDila>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(TypDila a, TypDila b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(TypDila a, TypDila b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion


        #region Implementation of ICollectionSynchronizer<Umisteni>

        /// <summary>
        /// Compares two entities and returns their natural order.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>-1, 0, 1.</returns>
        public int GetOrder(Umisteni a, Umisteni b)
        {
            return a.Id.CompareTo(b.Id);
        }

        /// <summary>
        /// Compares two entities and detects, if they are the same or not.
        /// </summary>
        /// <param name="a">A source entity (an entity in this collection).</param>
        /// <param name="b">A target entity (an entity in the other collection).</param>
        /// <returns>True if both entities are the same.</returns>
        public bool NeedsUpdate(Umisteni a, Umisteni b)
        {
            return a.NeedsUpdate(b);
        }

        #endregion
    }
}
