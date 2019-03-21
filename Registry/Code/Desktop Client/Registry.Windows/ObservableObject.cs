using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Registry.Windows
{
    /// <summary>
    /// An object that supports changenotification
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// raised when the value of a property has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises <see cref="PropertyChanged"/> for the property whose name matches the "propertyName" input
        /// </summary>
        /// 
        /// <param name="propertyName">
        /// [CallerMemberName] -> the property parameter can be decorated, and when property changed is called
        /// we can skip passing a value to use it as a default optional parameter
        /// the runtime will make sure that it will pass in the name of the property/method that we are calling from
        /// which is the set accessor of the property
        /// </param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            //if property changed is not null, then we raise our event handler that we passed in
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
