using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registry.Data;
using Registry.Data.Tests;
using Registry.DesktopClient.ViewModels;
using Registry.Windows;
using System.Linq;

namespace Registry.Desktopclient.Tests.UnitTests
{
    [TestClass]
    public class MainViewModelTests : FunctionalTest
    {
        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(MainViewModel).BaseType == typeof(ViewModel));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenNameIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = null,
                    MothersName = "test mother",
                    BirthPlace = "test city",
                    BirthDate = "test date",
                    TaxCode = "randomcode"
                }
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenMothersNameIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = "test person",
                    MothersName = null,
                    BirthPlace = "test city",
                    BirthDate = "test date",
                    TaxCode = "randomcode"
                }
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenBirthPlaceIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = "test person",
                    MothersName = "test mother",
                    BirthPlace = null,
                    BirthDate = "test date",
                    TaxCode = "randomcode"
                }
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenBirthDateIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = "test person",
                    MothersName = "test mother",
                    BirthPlace = "test city",
                    BirthDate = null,
                    TaxCode = "randomcode"
                }
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenTaxCodeIsNotValid()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = "test person",
                    MothersName = "test mother",
                    BirthPlace = "test city",
                    BirthDate = "test date",
                    TaxCode = null
                }
            };

            Assert.IsFalse(viewModel.AddPersonCommand.CanExecute(null));
        }

        [TestMethod]
        public void AddPersonCommendAddsPersonToPersonCollectionWhenExcecutedSuccessfully()
        {
            var viewModel = new MainViewModel
            {
                SelectedPerson = new Person
                {
                    PersonName = "test person",
                    MothersName = "test mother",
                    BirthPlace = "test city",
                    BirthDate = "test date",
                    TaxCode = "random code"
                }
            };

            viewModel.AddPersonCommand.Execute();
            Assert.IsTrue(viewModel.Persons.Count == 1);
        }

        [TestMethod]
        public void GetCustomerListCommandPopulatesPersonsProperty()
        {
            using (var context = new BusinessContext())
            {
                context.AddNewPerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });
                context.AddNewPerson(new Person { PersonName = "Test person2", MothersName = "test mom", BirthPlace = "test town", BirthDate = "1988", TaxCode = "xyz" });
                context.AddNewPerson(new Person { PersonName = "Test person3", MothersName = "test mum", BirthPlace = "test village", BirthDate = "1666", TaxCode = "abcabc" });

                var viewModel = new MainViewModel(context);
                viewModel.GetPersonListCommand.Execute(null);

                Assert.IsTrue(viewModel.Persons.Count == 3);
            }
        }

        [TestMethod]
        public void SavePersonCommand_UpdatesSelectedPersonName()
        {
            using (var context = new BusinessContext())
            {
                //Arrange
                context.AddNewPerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });

                var viewModel = new MainViewModel(context);

                viewModel.GetPersonListCommand.Execute(null);
                viewModel.SelectedPerson = viewModel.Persons.First();

                //Act
                viewModel.SelectedPerson.PersonName = "new value";
                viewModel.SavePersonCommand.Execute(null);

                //Assert
                var person = context.DataContext.Persons.Single();
                context.DataContext.Entry(person).Reload();
                Assert.AreEqual(viewModel.SelectedPerson.PersonName, person.PersonName);
            }
        }

        [TestMethod]
        public void DeletePersonCommand_DeletesPersonFromContext()
        {
            using (var context = new BusinessContext())
            {
                //Arrange
                context.AddNewPerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });

                var viewModel = new MainViewModel(context);

                viewModel.GetPersonListCommand.Execute(null);
                viewModel.SelectedPerson = viewModel.Persons.First();

                //Act
                viewModel.DeletePersonCommand.Execute(null);

                //Assert
                Assert.IsFalse(context.DataContext.Persons.Any());
            }
        }
    }
}
