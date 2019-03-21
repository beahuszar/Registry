using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Registry.Data;
using Registry.Data.Tests;
using Registry.DesktopClient.ViewModels;
using Registry.Windows;
using System.Collections.Generic;
using System.Linq;

namespace Registry.Desktopclient.Tests.UnitTests
{
    [TestClass]
    public class MainViewModelTests
    {
        private Mock<IBusinessContext> mock;
        private List<Person> store;

        [TestInitialize]
        public void TestInitialize()
        {
            //representative of our database - a list in memory
            store = new List<Person>();

            //creates a concrete type that implements the IBusinessContext, but has 0 default behaviour
            mock = new Mock<IBusinessContext>();

            //Setting up the behaviour of the signature

            //GetPersonList() should return a list of Persons declared as 'store'
            mock.Setup(m => m.GetPersonList()).Returns(store);

            //Takes a parameter of type 'Person', parameters does not matter
            //Whenever these methods are called, the 'Callback' definitions are called in fact
            //what is declared as a method in 'Callback' will be implemented during the tests
            mock.Setup(m => m.CreatePerson(It.IsAny<Person>())).Callback<Person>(person => store.Add(person));
            mock.Setup(m => m.DeletePerson(It.IsAny<Person>())).Callback<Person>(person => store.Remove(person));
            mock.Setup(m => m.UpdatePerson(It.IsAny<Person>())).Callback<Person>(person => 
            {
                int i = store.IndexOf(person);
                store[i] = person;
            });
        }

        [TestMethod]
        public void IsViewModel()
        {
            Assert.IsTrue(typeof(MainViewModel).BaseType == typeof(ViewModel));
        }

        [TestMethod]
        public void AddPersonCommandCannotExecuteWhenNameIsNotValid()
        {
            //'mock's' property called Object will store the instance that it creates of our IBusinessContext interface
            var viewModel = new MainViewModel(mock.Object)
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
            var viewModel = new MainViewModel(mock.Object)
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
            var viewModel = new MainViewModel(mock.Object)
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
            var viewModel = new MainViewModel(mock.Object)
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
            var viewModel = new MainViewModel(mock.Object)
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
            var viewModel = new MainViewModel(mock.Object)
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
            mock.Object.CreatePerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });
            mock.Object.CreatePerson(new Person { PersonName = "Test person2", MothersName = "test mom", BirthPlace = "test town", BirthDate = "1988", TaxCode = "xyz" });
            mock.Object.CreatePerson(new Person { PersonName = "Test person3", MothersName = "test mum", BirthPlace = "test village", BirthDate = "1666", TaxCode = "abcabc" });

            var viewModel = new MainViewModel(mock.Object);
            viewModel.GetPersonListCommand.Execute(null);

            Assert.IsTrue(viewModel.Persons.Count == 3);
            
        }

        /// <summary>
        /// No direct call to EF, but to ensure that our viewModel calls into the IBusinessContract
        /// </summary>
        [TestMethod]
        public void SaveCommand_InvokesIBusinessContextUpdatePersonMethod()
        {
            //Arrange
            mock.Object.CreatePerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetPersonListCommand.Execute(null);
            viewModel.SelectedPerson = viewModel.Persons.First();

            //Act
            viewModel.SelectedPerson.PersonName = "newValue";
            viewModel.SavePersonCommand.Execute(null);

            //Assert that the UpdatePerson method in fact invokes the SavePersonCommand
            mock.Verify(m => m.UpdatePerson(It.IsAny<Person>()), Times.Once);
        }

        [TestMethod]
        public void DeletePersonCommand_InvokesIBusinessContextDeletePersonMethod()
        {
            //Arrange
            mock.Object.CreatePerson(new Person { PersonName = "Test person", MothersName = "test mother", BirthPlace = "test city", BirthDate = "2018", TaxCode = "yadayada" });

            var viewModel = new MainViewModel(mock.Object);

            viewModel.GetPersonListCommand.Execute(null);
            viewModel.SelectedPerson = viewModel.Persons.First();

            //Act
            viewModel.DeletePersonCommand.Execute(null);

            //Assert
            mock.Verify(m => m.DeletePerson(It.IsAny<Person>()), Times.Once);
        }
    }
}
