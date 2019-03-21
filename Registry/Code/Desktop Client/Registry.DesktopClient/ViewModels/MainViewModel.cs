using Registry.Data;
using Registry.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Registry.DesktopClient.ViewModels
{
    /// <summary>
    /// Main application view-model
    /// </summary>
    public class MainViewModel : ViewModel
    {
        #region Private fields

        private readonly IBusinessContext context;
        private Person selectedPerson;

        #endregion

        #region Constructors

        public MainViewModel(IBusinessContext context)
        {
            Persons = new ObservableCollection<Person>();
            this.context = context;
        }

        #endregion

        /// <summary>
        /// Indicates whether the selected person is not null
        /// </summary>
        public bool CanModify
        {
            get
            {
                return SelectedPerson != null;
            }
        }

        /// <summary>
        /// Gets the collection of persons loaded from the data store
        /// </summary>
        public ICollection<Person> Persons { get; private set; }

        /// <summary>
        /// Getter & Setter for the selected person
        /// </summary>
        public Person SelectedPerson
        {
            get
            {
                return selectedPerson;
            }

            set
            {
                selectedPerson = value;
                NotifyPropertyChanged();
                //ReSharper disable once ExplicitCallerInfoArgument
                NotifyPropertyChanged("CanModify");
            }
        }

        /// <summary>
        /// Checks whether the view-model is valid
        /// </summary>
        /// <remarks>
        /// Now there isn't any actual validation happening. 
        /// This is because the wiring into the CommandManager.RequerySuggested in ActionCommand was removed because it is not the
        /// proper way to do commanding. It has unexpected behavior and severe performance implications, and should never be
        /// done.
        /// 
        /// To enable this again, another view-model needs to be implemented or the facade pattern used in place of using the
        /// person entity directly so that INotifyPropertyChanged can be implemented and ActionCommand.CanExecute(object) used
        /// when necessary, ultimately updating the commanding system, which will re-enable the enabling/disabling of UI elements
        /// based on the return value.
        /// </remarks>
        public bool isValid
        {
            get
            {
                return SelectedPerson == null ||
                    (!String.IsNullOrWhiteSpace(SelectedPerson.PersonName) &&
                     !String.IsNullOrWhiteSpace(SelectedPerson.MothersName) &&
                     !String.IsNullOrWhiteSpace(SelectedPerson.BirthPlace) &&
                     !String.IsNullOrWhiteSpace(SelectedPerson.BirthDate) &&
                     !String.IsNullOrWhiteSpace(SelectedPerson.TaxCode));
            }
        }
        
        /// <summary>
        /// Gets the command that invokes the creation of a new person
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                return new ActionCommand(
                    p => AddPerson(),
                    p => isValid);

            }
        }

        /// <summary>
        /// Gets the command that invokes the an update of the selected person
        /// </summary>
        public ICommand UpdateCommand
        {
            get
            {
                return new ActionCommand(
                    p => SavePerson(),
                    p => isValid);
            }
        }

        /// <summary>
        /// Gets the command that invokes the deletion of the selected person
        /// </summary>
        public ICommand DeleteCommand
        {
            get
            {
                return new ActionCommand(p => DeletePerson());
            }
        }

        /// <summary>
        /// Gets the command that invokes the retrieval of a list of person entities
        /// </summary>
        public ICommand GetPersonListCommand
        {
            get
            {
                return new ActionCommand(p => GetPersonList());
            }
        }

        private void AddPerson()
        {
           var person = new Person
           {
               PersonName = "default",
               MothersName = "default",
               BirthPlace = "default",
               BirthDate = "default",
               TaxCode = "default"
           };
           
           try
           {
               context.CreatePerson(person);
           }
           catch (Exception e)
           {
               //TODO: add exception catches (sql, ef)
               return;
           }
           
           Persons.Add(person);
        }

        private void SavePerson()
        {
            context.UpdatePerson(SelectedPerson);
        }

        private void DeletePerson()
        {
            context.DeletePerson(SelectedPerson);
            Persons.Remove(SelectedPerson);
            SelectedPerson = null;
        }

        private void GetPersonList()
        {
            Persons.Clear();

            foreach (var person in context.GetPersonList())
            {
                Persons.Add(person);
            }
        }
    }
}
