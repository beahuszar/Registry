using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registry.Data.Tests;
using Registry.DesktopClient.ViewModels;
using Registry.Windows;

namespace Registry.Desktopclient.Tests.UnitTests
{
    [TestClass]
    public class PersonViewModelTests : FunctionalTest
    {
        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(PersonViewModel).BaseType == typeof(ViewModel));
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

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenNameIsNotValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = null,
                MothersName = "test mother",
                BirthPlace = "test city",
                BirthDate = "test date",
                TaxCode = "randomcode"
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenMothersNameIsNotValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "test person",
                MothersName = null,
                BirthPlace = "test city",
                BirthDate = "test date",
                TaxCode = "randomcode"
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenBirthPlaceIsNotValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "test person",
                MothersName = "test mother",
                BirthPlace = null,
                BirthDate = "test date",
                TaxCode = "randomcode"
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenBirthDateIsNotValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "test person",
                MothersName = "test mother",
                BirthPlace = "test city",
                BirthDate = null,
                TaxCode = "randomcode"
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenTaxCodeIsNotValid()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "test person",
                MothersName = "test mother",
                BirthPlace = "test city",
                BirthDate = "test date",
                TaxCode = null
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommendAddsPersonToPersonCollectionWhenExcecutedSuccessfully()
        {
            var viewModel = new PersonViewModel
            {
                PersonName = "test person",
                MothersName = "test mother",
                BirthPlace = "test city",
                BirthDate = "test date",
                TaxCode = "random code"
            };

            viewModel.AddPersonCommand.Execute();
            Assert.IsTrue(viewModel.Persons.Count == 1);
        }
    }
}
