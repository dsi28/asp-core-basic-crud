using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class AppDbConext: DbContext
    {
        //create constructor
        public AppDbConext(DbContextOptions options) : base(options) {

        }
        // list where customer objects are going to be stored
        public DbSet<Customer> Customers { get; set; }
    }
}