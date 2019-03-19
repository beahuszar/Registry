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

        public Person AddNewPerson(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name must not be null");

            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name must not be empty string");

            var person = new Person
            {
                PersonName = name
            };

            context.Persons.Add(person);
            context.SaveChanges();

            return person;
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
