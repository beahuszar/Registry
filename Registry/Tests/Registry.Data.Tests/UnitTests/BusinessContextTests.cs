using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Registry.Data.Tests.UnitTests
{
    [TestClass]
    public class BusinessContextTests : FunctionalTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewPerson_ThrowsException_WhenNameIsNull()
        {
            using (var context = new BusinessContext())
            {
                var person = new Person
                {
                    PersonName = null,
                    MothersName = "test persons's mom",
                    BirthPlace = "test city",
                    BirthDate = "2018",
                    TaxCode = "randomcode"
                };
                context.AddNewPerson(person);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewPerson_ThrowsException_WhenNameIsEmpty()
        {
            using (var context = new BusinessContext())
            {
                var person = new Person
                {
                    PersonName = "",
                    MothersName = "test persons's mom",
                    BirthPlace = "test city",
                    BirthDate = "2018",
                    TaxCode = "randomcode"
                };
                context.AddNewPerson(person);
            }
        }

        [TestMethod]
        public void UpdatePerson_ChangedValuesAreApplied()
        {
            using (var bc = new BusinessContext())
            {
                //Arrange
                var person = new Person
                {
                    PersonName = "test name",
                    MothersName = "test persons's mom",
                    BirthPlace = "test city",
                    BirthDate = "2018",
                    TaxCode = "randomcode"
                };

                bc.AddNewPerson(person);

                const string
                    newPersonName = "new name",
                    newMothersName = "new mother",
                    newBirthPlace = "new birthPlace",
                    newBirthDate = "new birthDate",
                    newTaxCode = "new taxCode";

                person.PersonName = newPersonName;
                person.MothersName = newMothersName;
                person.BirthPlace = newBirthPlace;
                person.BirthDate = newBirthDate;
                person.TaxCode = newTaxCode;

                //Act
                bc.UpdatePerson(person);

                //Assert
                bc.DataContext.Entry(person).Reload();

                Assert.AreEqual(newPersonName, person.PersonName);
                Assert.AreEqual(newMothersName, person.MothersName);
                Assert.AreEqual(newBirthPlace, person.BirthPlace);
                Assert.AreEqual(newBirthDate, person.BirthDate);
                Assert.AreEqual(newTaxCode, person.TaxCode);
            }
        }

        [TestMethod]
        public void GetPersonList_ReturnsExpectedPersons()
        {
            using (var bc = new BusinessContext())
            {
                bc.AddNewPerson(new Person { PersonName = "xy", MothersName = "mom", BirthPlace = "city", BirthDate = "now", TaxCode = "code" });
                bc.AddNewPerson(new Person { PersonName = "test", MothersName = "mommy", BirthPlace = "town", BirthDate = "then", TaxCode = "anothercode" });
                bc.AddNewPerson(new Person { PersonName = "someoneelse", MothersName = "mum", BirthPlace = "village", BirthDate = "never", TaxCode = "verycode" });

                var customers = bc.GetPersonList();

                Assert.IsTrue(customers.ElementAt(0).Id == 1);
                Assert.IsTrue(customers.ElementAt(1).Id == 2);
                Assert.IsTrue(customers.ElementAt(2).Id == 3);

                //Use CollectionAssert by overriding Equals in entities
                //or using IComparer in CollectionAssert for a more full-proof solution
            }
        }
    }
}
