using Registry.Data;
using Registry.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Registry.DesktopClient.ViewModels
{
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
            }
        }

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
        
        public ActionCommand AddPersonCommand
        {
            get
            {
                return new ActionCommand(
                    p => AddPerson(),
                    p => isValid);
            }
        }

        public ActionCommand SavePersonCommand
        {
            get
            {
                return new ActionCommand(
                    p => SavePerson(),
                    p => isValid);
            }
        }

        public ActionCommand DeletePersonCommand
        {
            get
            {
                return new ActionCommand(
                    p => DeletePerson());
            }
        }

        public ICollection<Person> Persons { get; private set; }

        public ICommand AddCommand
        {
            get
            {
                return new ActionCommand(
                    p => AddPerson(),
                    p => isValid);

            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new ActionCommand(
                    p => SavePerson(),
                    p => isValid);
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new ActionCommand(p => DeletePerson());
            }
        }

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
