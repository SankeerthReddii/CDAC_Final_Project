using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Metro_Ticket_Project.Infrastructure.Data;

namespace Metro_Ticket_Project
{
    public class MetroDbContextFactory : IDesignTimeDbContextFactory<MetroDbContext>
    {
        public MetroDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MetroDbContext>();
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-2J307PJ3\\SQLEXPRESS;Initial Catalog=MetroOne;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;"
);


            return new MetroDbContext(optionsBuilder.Options);
        }
    }
}
