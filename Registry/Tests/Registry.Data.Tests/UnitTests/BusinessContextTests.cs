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
                context.AddNewPerson(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewPerson_ThrowsException_WhenNameIsEmpty()
        {
            using (var context = new BusinessContext())
            {
                context.AddNewPerson("");
            }
        }
    }
}
