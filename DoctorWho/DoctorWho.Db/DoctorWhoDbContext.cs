using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DoctorWho.Db
{
    public class DoctorWhoDbContext : DbContext
    {
        public DoctorWhoDbContext()
        {
        }

        public DoctorWhoDbContext(DbContextOptions<DoctorWhoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = DoctorWhoCore")
                    .LogTo(Console.WriteLine, new[]
                    {DbLoggerCategory.Database.Command.Name },
                     LogLevel.Information).EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}