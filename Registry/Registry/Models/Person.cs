using System.Collections.Generic;

namespace Registry
{
    public class Person
    {
        public long PersonId { get; set; }
        public string PersonName { get; set; }
        //to be changed to Date
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string MothersName { get; set; }
        public string TaxCode { get; set; }

        public List<PersonAddress> PersonAddress { get; set; }
    }
}
