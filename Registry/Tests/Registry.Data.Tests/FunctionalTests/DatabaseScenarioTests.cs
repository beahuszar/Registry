using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Registry.Data.Tests.FunctionalTests
{
    [TestClass]
    public class DatabaseScenarioTests
    {
        [TestMethod]
        public void CanCreateDatabase()
        {
            using (var db = new DomainContext())
            {
                db.Database.Create();
            }
        }

    }
}
