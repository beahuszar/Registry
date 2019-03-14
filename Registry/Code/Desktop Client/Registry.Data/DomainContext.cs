using System.Data.Entity;

namespace Registry.Data
{
    public class DomainContext : DbContext
    {
        public DomainContext() : base("name=Default")
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
