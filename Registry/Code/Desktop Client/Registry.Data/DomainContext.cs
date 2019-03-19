using System.Data.Entity;

namespace Registry.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=Default")
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
