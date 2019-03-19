using Registry.Data;
using Registry.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Registry.DesktopClient.ViewModels
{
    public class PersonViewModel : ViewModel
    {
        #region Private fields

        private string personName;

        #endregion

        #region Constructor

        public PersonViewModel()
        {
            this.Persons = new ObservableCollection<Person>();
        }

        #endregion

        [Required]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "not enough caracters")]
        public string PersonName
        {
            get { return personName; }
            set
            {
                personName = value;
                NotifyPropertyChanged();
            }
        }

        public string MothersName { get; set; }
        public string BirthPlace { get; set; }
        //to be changed to Date
        public string BirthDate { get; set; }
        public string TaxCode { get; set; }

        public bool isValid
        {
            get
            {
                return
                    !String.IsNullOrWhiteSpace(PersonName) &&
                    !String.IsNullOrWhiteSpace(MothersName) &&
                    !String.IsNullOrWhiteSpace(BirthPlace) &&
                    !String.IsNullOrWhiteSpace(BirthDate) &&
                    !String.IsNullOrWhiteSpace(TaxCode);
            }
        }
        
        public ActionCommand AddPersonCommand
        {
            get
            {
                return new ActionCommand(
                    p => AddPerson(PersonName, MothersName, BirthPlace, BirthDate, TaxCode),
                    p => isValid);
            }
        }

        public ICollection<Person> Persons { get; private set; }

        private void AddPerson (string personName, string mothersName, string birthPlace, string birthDate, string taxCode)
        {
           using (var api = new BusinessContext())
           {
               var person = new Person
               {
                   PersonName = personName,
                   MothersName = mothersName,
                   BirthPlace = birthDate,
                   BirthDate = birthDate,
                   TaxCode = taxCode
               };
           
               try
               {
                   api.AddNewPerson(person);
               }
               catch (Exception e)
               {
                   //TODO: add exception catches (sql, ef)
                   return;
               }
           
               Persons.Add(person);
           }
        }
    }
}
