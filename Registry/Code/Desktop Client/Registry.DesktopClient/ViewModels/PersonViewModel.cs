using Registry.Windows;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Registry.DesktopClient.ViewModels
{
    public class PersonViewModel : ViewModel
    {
        #region Private fields

        private string personName;

        #endregion

        #region Public Properties

        public long PersonId { get; set; }

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

        //to be changed to Date
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string MothersName { get; set; }
        public string TaxCode { get; set; }
        #endregion
    }
}
