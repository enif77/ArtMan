using System;

using CsvDb;
using CvsDbTest.Core.Injektor;
using CvsDbTest.Datalayer;
using CvsDbTest.DataObjects;

namespace CvsDbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\Devel\Projects\Personal\CS\ArtMan\CvsDbTest\Data\Mena\1_Mena.txt";

            try
            {
                //var ent = FileHelper.LoadDataEntity(path);
                //Console.WriteLine(ent.Values.Keys);
                
                var db = new Database(@"D:\Devel\Projects\Personal\CS\ArtMan\CvsDbTest\Data");
                Initializer.InitializeLayers(db);

                var dal = Registry.Get<MenaDataLayer>();

                //var meny = dal.GetAll();
                //Console.WriteLine(meny.Count());

                var mena = dal.Get(1);
                Console.WriteLine(mena.Name);

                //var e = mena.CreateDataEntity();

                //mena.Name += " xxx";
                //dal.Save(mena);

                //mena = dal.Reload(1);
                //Console.WriteLine(mena.Name);

                mena = new Mena();
                mena.Name = "XXX";
                mena.Popis = "Brexit";
                dal.Save(mena);
                Console.WriteLine(mena.Name);

                mena = dal.Get(mena.Id);
                Console.WriteLine(mena.Name);
                dal.Delete(mena);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("DONE");
            }
        }
    }
}
