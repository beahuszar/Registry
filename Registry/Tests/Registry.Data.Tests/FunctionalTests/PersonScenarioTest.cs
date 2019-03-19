using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Registry.Data.Tests.FunctionalTests
{
    [TestClass]
    public class PersonScenarioTest : FunctionalTest
    {
        [TestMethod]
        public void AddNewPersonIsPersisted()
        {
            using (var bc = new BusinessContext())
            {
                //assuming only name is mandatory
                Person entity = bc.AddNewPerson("TestPerson");

                bool exists = bc.DataContext.Persons.Any(person => person.Id == entity.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}
