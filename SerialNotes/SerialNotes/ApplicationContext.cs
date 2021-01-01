using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SerialNotes.Models;

namespace SerialNotes
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        public DbSet<NotesSQL> NotesSQL { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<SerialsSQL> Serials { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<StatsAvg> StatsAvg { get; set; }

      
        public DbSet<PartsSerial> PartsSerial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stats>(stat => stat.HasNoKey());
            modelBuilder.Entity<StatsAvg>(stat => stat.HasNoKey());
            modelBuilder.Entity<SerialsSQL>(SerialConfiguration);
        }

        public void SerialConfiguration(EntityTypeBuilder<SerialsSQL> serial)
        {
            serial.Property(s => s.SerialId).HasColumnName("SerialId").HasColumnType("int(11)").ValueGeneratedOnAdd();
        }

    }
}
