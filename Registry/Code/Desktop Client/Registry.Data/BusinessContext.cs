using System;

namespace Registry.Data
{
    /// <summary>
    /// Making it sealed, because we do not want people to get around business rules
    /// </summary>
    public sealed class BusinessContext : IDisposable
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

        public void AddNewPerson(Person person)
        {
            person.PersonName.CheckStringParameters();
            context.Persons.Add(person);
            context.SaveChanges();
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
