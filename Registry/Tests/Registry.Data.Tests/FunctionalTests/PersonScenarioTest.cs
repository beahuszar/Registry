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
                var person = new Person
                {
                    PersonName = "test person",
                    MothersName = "test persons's mom",
                    BirthPlace = "test city",
                    BirthDate = "2018",
                    TaxCode = "randomcode"
                };

                bc.CreatePerson(person);

                bool exists = bc.DataContext.Persons.Any(p => p.Id == person.Id);

                Assert.IsTrue(exists);
            }
        }

        [TestMethod]
        public void DeletePerson_RemovesPersonFromDataStore()
        {
            using (var bc = new BusinessContext())
            {
                var person = new Person
                {
                    PersonName = "test person",
                    MothersName = "test persons's mom",
                    BirthPlace = "test city",
                    BirthDate = "2018",
                    TaxCode = "randomcode"
                };

                bc.CreatePerson(person);
                bc.DeletePerson(person);
                Assert.IsFalse(bc.DataContext.Persons.Any());
            }
        }
    }
}
