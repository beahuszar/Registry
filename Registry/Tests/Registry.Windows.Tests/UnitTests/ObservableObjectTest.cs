using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Registry.Windows.Tests.UnitTests
{
    [TestClass]
    public class ObservableObjectTest
    {
        [TestMethod]
        public void PropertyChangedEventHandlerIsRaised()
        {
            var obj = new StubObservableObject();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            //allow to invoke the property changed event
            obj.ChangedProperty = "Some value";

            if (!raised) Assert.Fail("PropertyChanged was never invoked");
        }

        
    }

    internal class StubObservableObject : ObservableObject
    {
        private string changedProperty;

        public string ChangedProperty
        {
            get
            {
                return changedProperty;
            }
            set
            {
                changedProperty = value;
                //used from base class
                NotifyPropertyChanged();
            }
        }
    }
}
