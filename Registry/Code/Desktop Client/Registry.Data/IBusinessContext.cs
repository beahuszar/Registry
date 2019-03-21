using System.Collections.Generic;

namespace Registry.Data
{
    public interface IBusinessContext
    {
        /// <summary>
        /// Adds a new Person entity to the data store
        /// </summary>
        void CreatePerson(Person person);

        /// <summary>
        /// Deletes the specified person
        /// </summary>
        void DeletePerson(Person person);

        /// <summary>
        /// Updates the specified person by applying the values passed in over the existing values from the data store
        /// </summary>
        /// <param name="person"> the person entity containing the new values to persist </param>
        void UpdatePerson(Person person);

        /// <summary>
        /// Gets a collection of customers from the data store and returns them ordered by primary key
        /// </summary>
        ICollection<Person> GetPersonList();
    }
}