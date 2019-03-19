using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Registry.Data.Tests
{
    [TestClass]
    public class FunctionalTest
    {
        [TestInitialize]
        public virtual void TestInitialize()
        {
            using (var db = new DataContext())
            { 
                if (db.Database.Exists())
                    db.Database.Delete();

                db.Database.Create();
            }
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            using (var db = new DataContext())
                if (db.Database.Exists())
                    db.Database.Delete();
        }
    }
}
