using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DoctorWho.Db
{
    public class DoctorWhoDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Companion> Companions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<EpisodeCompanion> EpisodeCompanions { get; set; }
        public DbSet<EpisodeEnemy> EpisodeEnemies { get; set; }

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
            //Enemy Shadow Properties
            modelBuilder.Entity<Enemy>().Property<string>(e => e.EnemyName).HasMaxLength(30);
            modelBuilder.Entity<Enemy>().Property<string>(e => e.Description).HasDefaultValue("Null");

            //Companion Shadow Properties
            modelBuilder.Entity<Companion>().Property<string>(c => c.CompanionName).HasMaxLength(30);
            modelBuilder.Entity<Companion>().Property<string>(c => c.WhoPlayed).HasMaxLength(30);

            //Doctor Shadow Properties
            modelBuilder.Entity<Doctor>().Property<int>(d => d.DoctorNumber).IsRequired();
            modelBuilder.Entity<Doctor>().Property<string>(d => d.DoctorName).HasMaxLength(30);
            modelBuilder.Entity<Doctor>().Property<DateTime>(d => d.BirthDate).HasDefaultValueSql("Null");
            modelBuilder.Entity<Doctor>().Property<DateTime>(d => d.FirstEpisodeDate).HasDefaultValueSql("Null");
            modelBuilder.Entity<Doctor>().Property<DateTime>(d => d.LastEpisodeDate).HasDefaultValueSql("Null");

            //Author Shadow Properties
            modelBuilder.Entity<Author>().Property<string>(a => a.AuthorName).HasMaxLength(30);

            //Episode Shadow Properties
            modelBuilder.Entity<Episode>().Property<int>(e => e.SeriesNumber).HasDefaultValue(0);
            modelBuilder.Entity<Episode>().Property<int>(e => e.EpisodeNumber).HasDefaultValue(0);
            modelBuilder.Entity<Episode>().Property<string>(e => e.EpisodeType).HasMaxLength(30);
            modelBuilder.Entity<Episode>().Property<string>(e => e.Title).HasDefaultValue("Null");
            modelBuilder.Entity<Episode>().Property<DateTime>(e => e.EpisodeDate).HasDefaultValueSql("Null");
            modelBuilder.Entity<Episode>().Property<string>(e => e.Notes).HasDefaultValue("Null");
        }
    }
}