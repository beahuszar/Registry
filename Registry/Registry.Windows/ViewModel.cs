using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Registry.Windows
{
    /// <summary>
    /// If we have a property that is decorated with a data validation attribute
    /// we should get a validation error from the index or property
    /// if it is valid, it should be a null or empty string
    /// </summary>
    public abstract class ViewModel : ObservableObject, IDataErrorInfo
    {
        //an indexer with a string parameter
        public string this[string columnName]
        {
            get { return OnValidate(columnName); }
        }

        //property
        public string Error => throw new System.NotImplementedException();

        //our requirement: we want to be able to override this & deriving view Models if we need that type of extensibility
        protected virtual string OnValidate(string columnName)
        {
            //Takes an instance of the object we are validating as a parameter
            var context = new ValidationContext(this)
            {
                MemberName = columnName
            };

            //ValidationResults: objects that encaps ulate the validation information, that is returned back from a method called TryValidateObject below
            var results = new Collection<ValidationResult>();

            //Validator: a helper class that's part of the data annotations framework
            //this: the object we are validating
            //context: gives an information about what we are validating
            //results: the method stores the validation results in the collection we have created
            //true: validate all properties
            var isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
                foreach (var result in results)
                    foreach (var member in result.MemberNames)
                        if (member == columnName && result.ErrorMessage != null)
                            return result.ErrorMessage;

            return null;
        }
    }
}
