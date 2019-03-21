using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Registry.Windows
{
    /// <summary>
    /// Base class for a view-model in the MVVM pattern
    /// </summary>
    public abstract class ViewModel : ObservableObject, IDataErrorInfo
    {
        /// <summary>
        /// an indexer with a string parameter
        /// Gets the validation error for a property whose name matches the "columnName" input. 
        /// </summary>
        /// 
        /// <param name="columnName"> The name of the property to validate </param>
        /// <returns> Returns a validation error if there is one, otherwise returns null or empty string </returns>
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }

        /// <summary>
        /// Not supported
        /// </summary>
        [Obsolete]
        public string Error => throw new NotSupportedException();

        /// <summary>
        /// Validates a property whose name matches the "propertyName" input
        /// </summary>
        /// 
        /// <param name="propertyName"> the name of the property to validate </param>
        /// <returns> returns a validation error, if any, otherwise returns null </returns>
        protected virtual string OnValidate(string propertyName)
        {
            //Takes an instance of the object we are validating as a parameter
            var context = new ValidationContext(this)
            {
                MemberName = propertyName
            };

            //ValidationResults: objects that encapsulate the validation information, that is returned back from a method called TryValidateObject below
            var results = new Collection<ValidationResult>();

            //Validator: a helper class that's part of the data annotations framework
            //this: the object we are validating
            //context: gives an information about what we are validating
            //results: the method stores the validation results in the collection we have created
            //true: validate all properties
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                ValidationResult result = results
                    .SingleOrDefault(p => p.MemberNames.Any(membername => membername == propertyName));

                return result == null ? null : result.ErrorMessage;
            }

            return null;
        }
    }
}
