using System.Collections.Generic;

namespace Registry.Data
{
    public interface IBusinessContext
    {
        void CreatePerson(Person person);
        ICollection<Person> GetPersonList();
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
    }
}