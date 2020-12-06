using Microsoft.EntityFrameworkCore;
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
        public DbSet<Serials> Serials { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<StatsAvg> StatsAvg { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stats>(stat => stat.HasNoKey());
            modelBuilder.Entity<StatsAvg>(stat => stat.HasNoKey());
        }

    }
}
