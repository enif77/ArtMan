/* (C) 2016 Premysl Fara */

namespace ArtMananager.Datalayer
{
    using System;
    using System.Collections.Generic;

    using SimpleDb.Files;
    using Injektor;

    using ArtMananager.DataObjects;


    public class DiloDataLayer : LookupDataLayer<Dilo>
    {
        public DiloDataLayer(Database database) 
            : base(database)
        {
        }


        public List<DiloDataGridItem> GetAll(DiloFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");

            GetAll();

            var res = new List<DiloDataGridItem>();

            var autorDal = Registry.Get<AutorDataLayer>();
            var majitelDal = Registry.Get<MajitelDataLayer>();
            var menaDal = Registry.Get<MenaDataLayer>();
            var prodejniMistoDal = Registry.Get<ProdejniMistoDataLayer>();
            var technikaDal = Registry.Get<TechnikaDataLayer>();
            var typDilaDal = Registry.Get<TypDilaDataLayer>();
            var umisteniDal = Registry.Get<UmisteniDataLayer>();

            int rowAcc = 0;
            int diloAcc = 0;
            decimal nakupCenaCzkAcc = 0;
            decimal dnesniCenaCzkAcc = 0;

            foreach (var d in DataObjects.Values)
            {
                if (filter.KoupenoOdRok.HasValue && d.KoupenoKdyRok < filter.KoupenoOdRok.Value) continue;
                if (filter.KoupenoDoRok.HasValue && d.KoupenoKdyRok > filter.KoupenoDoRok.Value) continue;
                if (filter.Prodano == 0 && d.Prodano) continue;
                if (filter.Prodano == 1 && d.Prodano == false) continue;
                if (filter.AutorId != 0 && filter.AutorId != d.AutorId) continue;
                if (filter.UmisteniId != 0 && filter.UmisteniId != d.UmisteniId) continue;
                if (filter.KoupenoKdeId != 0 && filter.KoupenoKdeId != d.KoupenoKdeId) continue;
                if (filter.MajitelId != 0 && filter.MajitelId != d.MajitelId) continue;

                var i = new DiloDataGridItem
                {
                    Id = d.Id,
                    KoupenoRok = d.KoupenoKdyRok,
                    NakupCena = d.NakupCena,
                    NakupMenaNazev = GetMenaNazev(menaDal, d.NakupMenaId),
                    NakupCenaCzk = d.NakupCenaCzk,
                    DnesniCenaCzk = d.DnesniCenaCzk,
                    KoupenoKdeNazev = GetProdejniMistoNazev(prodejniMistoDal, d.KoupenoKdeId),
                    AutorJmeno = GetAutorJmeno(autorDal, d.AutorId),
                    AutorPrijmeni = GetAutorPrijmeni(autorDal, d.AutorId),
                    Nazev = d.Name,
                    Rok = d.Rok,
                    TypDilaNazev = GetTypDilaNazev(typDilaDal, d.TypDilaId),
                    TechnikaNazev = GetTechnikaNazev(technikaDal, d.TechnikaId),
                    Rozmer = d.Rozmer,
                    Skladem = d.Skladem,
                    Pojisteno = d.Pojisteno,
                    Prodano = d.Prodano,
                    UmisteniNazev = GetUmisteniNazev(umisteniDal, d.UmisteniId),
                    Majitel = GetMajitel(majitelDal, d.MajitelId),
                    Kusu = d.Kusu,
                };

                res.Add(i);

                rowAcc++;
                diloAcc += d.Kusu;
                nakupCenaCzkAcc += d.NakupCenaCzk;
                dnesniCenaCzkAcc += d.DnesniCenaCzk;
            }

            filter.RowAcc = rowAcc;
            filter.DiloAcc = diloAcc;
            filter.NakupCenaCzkAcc = nakupCenaCzkAcc;
            filter.DnesniCenaCzkAcc = dnesniCenaCzkAcc;

            return res;
        }


        private string GetAutorJmeno(AutorDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Jmeno;
            }

            return String.Empty;
        }

        private string GetAutorPrijmeni(AutorDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Prijmeni;
            }

            return String.Empty;
        }
              
        private string GetMajitel(MajitelDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }
              
        private string GetMenaNazev(MenaDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetProdejniMistoNazev(ProdejniMistoDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetTechnikaNazev(TechnikaDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetTypDilaNazev(TypDilaDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }

        private string GetUmisteniNazev(UmisteniDataLayer dal, int id)
        {
            if (id > 0)
            {
                var d = dal.Get(id);
                return (d == null) ? String.Empty : d.Name;
            }

            return String.Empty;
        }


        public override int GetIdByName(string name, bool bypassCache = false)
        {
            throw new NotImplementedException();
        }


        private readonly DateTime _minSqlDate = new DateTime(1900, 1, 1);


        public override int Save(Dilo obj)
        {
            // TODO: DB shoud allow nulls for dates.
            
            if (obj.KoupenoKdy == null || obj.KoupenoKdy < _minSqlDate)
            {
                obj.KoupenoKdy = _minSqlDate;
            }

            //if (obj.ProdanoKdy == null || obj.ProdanoKdy < _minSqlDate)
            //{
            //    obj.ProdanoKdy = _minSqlDate;
            //}

            return base.Save(obj);
        }
    }
}
