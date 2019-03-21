using System;
using System.Collections.Generic;
using System.Linq;

namespace Registry.Data
{
    /// <summary>
    /// Encapsulates business rules when accessing the data layer
    /// </summary>
    public sealed class BusinessContext : IDisposable, IBusinessContext
    {
        #region Private fields, public properties & constructors

        private readonly DataContext context;
        private bool disposed;

        public BusinessContext()
        {
            this.context = new DataContext();
        }

        /// <summary>
        /// Getter for context
        /// </summary>
        public DataContext DataContext
        {
            get { return context;  }
        }

        #endregion

        /// <summary>
        /// Adds a new Person entity to the data store
        /// </summary>
        public void CreatePerson(Person person)
        {
            person.PersonName.CheckStringParameters();

            context.Persons.Add(person);
            context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified person by applying the values passed in over the existing values from the data store
        /// </summary>
        /// <param name="person"> the person entity containing the new values to persist </param>
        public void UpdatePerson(Person person)
        {
            var entity = context.Persons.Find(person.Id);
            if (entity == null)
            {
                throw new NotImplementedException("Handle appropriately for your API design");
            }

            context.Entry(person).CurrentValues.SetValues(person);
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified person
        /// </summary>
        public void DeletePerson(Person person)
        {
            context.Persons.Remove(person);
            context.SaveChanges();
        }

        /// <summary>
        /// Gets a collection of customers from the data store and returns them ordered by primary key
        /// </summary>
        public ICollection<Person> GetPersonList()
        {
            return context.Persons.OrderBy(p => p.Id).ToArray();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing or resetting unmanaged resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing)
                return;

            if (context != null)
                context.Dispose();

            disposed = true;
        }

        #endregion
    }
}
