using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Registry.Windows.Tests.UnitTests
{
    [TestClass]
    public class ViewModelTest
    {
        /// <summary>
        /// We do not want this class to be instanciated, thus it should be abstract
        /// </summary>
        [TestMethod]
        public void IsAbstractBaseClass()
        {
            Type t = typeof(ViewModel);

            Assert.IsTrue(t.IsAbstract);
        }

        [TestMethod]
        public void IsIDataErrorInfoImplemented()
        {
            Assert.IsTrue(typeof(IDataErrorInfo).IsAssignableFrom(typeof(ViewModel)));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IDataErrorInfo_ErrorProperty_IsNotImplemented()
        {
            var viewModel = new StubViewModel();
            var value = viewModel.Error;
        }

        [TestMethod]
        public void IndexerPropertyValidatesPropertyNameWithInvalidValue()
        {
           var viewModel = new StubViewModel();
            //class has to have a property called PropertyToBeValidated and it cannot be null
            Assert.IsNotNull(viewModel["PropertyToBeValidated"]);
        }
        
        [TestMethod]
        public void IndexerPropertyValidatesPropertyNameWithValidValue()
        {
            var viewModel = new StubViewModel
            {
                PropertyToBeValidated = "dummy value"
            };

            //we are expecting no exception as we are providing valid values
            Assert.IsNull(viewModel["PropertyToBeValidated"]);
            Assert.IsTrue(viewModel.PropertyToBeValidated.Equals("dummy value"));
        }

        [TestMethod]
        public void IsObservableObjectExtended()
        {
            var viewModel = new StubViewModel();
            Assert.IsTrue(typeof(ViewModel).BaseType == typeof(ObservableObject));
        }

        [TestMethod]
        public void IndexerReturnsErrorMessageForRequestedInvalidProperty()
        {
            var viewModel = new StubViewModel
            {
                PropertyToBeValidated = null,
                SomeOtherProperty = null
            };

            var msg = viewModel["SomeOtherProperty"];

            Assert.AreEqual("The SomeOtherProperty field is required.", msg);
        }
    }

    internal class StubViewModel : ViewModel
    {
        [Required]
        public string PropertyToBeValidated { get; set; }

        [Required]
        public string SomeOtherProperty { get; set; }
    }
}
