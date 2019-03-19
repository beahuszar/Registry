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

                bc.AddNewPerson(person);

                bool exists = bc.DataContext.Persons.Any(p => p.Id == person.Id);

                Assert.IsTrue(exists);
            }
        }
    }
}
