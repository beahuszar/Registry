using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registry.DesktopClient.ViewModels;
using Registry.Windows;

namespace Registry.Desktopclient.Tests.UnitTests
{
    [TestClass]
    public class PersonViewModelTests
    {
        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(PersonViewModel).BaseType == typeof(ViewModel));
        }

        [TestMethod]
        public void ValidationErrorWhenPersonNameExceeds10Characters()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "lfjsdoifjsdoifjsdofijsdfiosjdf"
            };

            Assert.IsNotNull(viewModel["PersonName"]);
        }

        [TestMethod]
        public void ValidationErrorWhenPersonNameLengthIsShorterThan4Characters()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "abc"
            };

            Assert.IsNotNull(viewModel["PersonName"]);
        }

        [TestMethod]
        public void PersonNameIsValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "ValidName"
            };

            Assert.IsNull(viewModel["PersonName"]);
            Assert.IsNotNull(viewModel.PersonName);
            Assert.IsTrue(viewModel.PersonName.Equals("ValidName"));
        }
    }
}
