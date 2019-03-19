using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Registry.Data.Tests.UnitTests
{
    [TestClass]
    public class BusinessContextTests
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
    }
}
