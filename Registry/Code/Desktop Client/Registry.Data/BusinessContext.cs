using System;
using System.Collections.Generic;
using System.Linq;

namespace Registry.Data
{
    /// <summary>
    /// Making it sealed, because we do not want people to get around business rules
    /// </summary>
    public sealed class BusinessContext : IDisposable, IBusinessContext
    {
        private readonly DataContext context;
        private bool disposed;

        public BusinessContext()
        {
            this.context = new DataContext();
        }

        public DataContext DataContext
        {
            get { return context;  }
        }

        public void CreatePerson(Person person)
        {
            person.PersonName.CheckStringParameters();
            context.Persons.Add(person);
            context.SaveChanges();
        }

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

        public void DeletePerson(Person person)
        {
            context.Persons.Remove(person);
            context.SaveChanges();
        }

        public ICollection<Person> GetPersonList()
        {
            return context.Persons.OrderBy(p => p.Id).ToArray();
        }

        #region Disposable Members

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
            if (disposed || disposing)
                return;

            if (context != null)
                context.Dispose();

            disposed = true;
        }
        #endregion
    }
}
