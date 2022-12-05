/* (C) 2016 Premysl Fara */

namespace CvsDbTest.Datalayer
{
    using System;
    using System.Collections.Generic;

    using CsvDb;
    using CvsDbTest.DataObjects;


    public class DiloDataLayer : LookupDataLayer<Dilo>
    {
        public DiloDataLayer(Database database) 
            : base(database)
        {
        }


        public List<DiloDataGridItem> GetAll(DiloFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");

            //var parameters = new List<SqlParameter>
            //{
            //    new SqlParameter("@KoupenoOd", (filter.UseKoupenoOd) ? filter.KoupenoOd : (object) DBNull.Value),
            //    new SqlParameter("@KoupenoDo", (filter.UseKoupenoDo) ? filter.KoupenoDo : (object) DBNull.Value),
            //    new SqlParameter("@ProdanoOd", (filter.UseProdanoOd) ? filter.ProdanoOd : (object) DBNull.Value),
            //    new SqlParameter("@ProdanoDo", (filter.UseProdanoDo) ? filter.ProdanoDo : (object) DBNull.Value),
            //    new SqlParameter("@Skladem", filter.Skladem),
            //    new SqlParameter("@JeProdano", filter.Prodano),
            //    new SqlParameter("@AutorId", filter.AutorId)
            //};

            var res = new List<DiloDataGridItem>();

            //using (ApplicationTasksManager.Instance.StartTask("Reading..."))
            //{
            //    var consumer = new DataConsumer<DiloDataGridItem>(res);

            //    Database.ExecuteReader(
            //       "spDilo_SelectList",
            //       parameters.ToArray(),
            //       consumer.CreateInstance,
            //       null);
            //}

            return res;
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
