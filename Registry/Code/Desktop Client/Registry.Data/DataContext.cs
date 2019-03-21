using System.Data.Entity;

namespace Registry.Data
{
    /// <summary>
    /// Encapsulates database access
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Constructor for Datacontext that will use the connection string "default" from the application's configuration file
        /// </summary>
        public DataContext() : base("name=Default")
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
