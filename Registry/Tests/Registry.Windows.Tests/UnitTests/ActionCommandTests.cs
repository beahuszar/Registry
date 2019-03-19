using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Registry.Windows.Tests.UnitTests
{
    [TestClass]
    public class ActionCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            var command = new ActionCommand(null);
        }

        /// <summary>
        /// make sure when excecute calls are made on the ICommand interface 
        /// it invokes the delegate that we passed into it
        /// </summary>
        [TestMethod]
        public void ExecuteInvokesAction()
        {
            var invoked = false;

            //when our action is excecuted, invoked is set to true
            Action<Object> action = obj => invoked = true;

            //then we are passing the action command into the ActionCommand's const
            var command = new ActionCommand(action);

            //...then it will excecute it
            //By default on an ICommand implementation an explicit null value has to be passed for the parameter on excecute
            //thus come convenience methods are provided to get round out
            command.Execute();

            //if indeed excecuted, action will be set to true, as defined in the implementation
            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void ExecuteOverloadInvokesActionWithParameter()
        {
            //similar logic as in InvokesAction(), but with parameter this time

            var invoked = false;

            Action<Object> action = obj =>
            {
                Assert.IsNotNull(obj);
                invoked = true;
            };

            var command = new ActionCommand(action);

            command.Execute(new object());

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new ActionCommand(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteOverloadExcecutesTruePredicate()
        {
            //check if passed in value == 1 
            var command = new ActionCommand(obj => { }, obj => (int)obj == 1);
            //...passing in 1
            Assert.IsTrue(command.CanExecute(1));
        }

        [TestMethod]
        public void CanExecuteOverloadExcecutesFalsePredicate ()
        {
            //check if passed in value == 1 
            var command = new ActionCommand(obj => { }, obj => (int)obj == 1);
            //...passing in 0
            Assert.IsFalse(command.CanExecute(0));
        }
    }
}
