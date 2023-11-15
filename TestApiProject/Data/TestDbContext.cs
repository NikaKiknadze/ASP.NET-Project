using Microsoft.EntityFrameworkCore;
using TestApiProject.Entities;

namespace TestApiProject.Data
{
    public class TestDbContext: DbContext
    {
        public TestDbContext()
        {
            
        }
        public TestDbContext(DbContextOptions<TestDbContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<SuperPowers> SuperPowers { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<CharactersSuperpowersJoin> CharactersSuperpowersJoin { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharactersSuperpowersJoin>()
                .HasKey(n => new { n.SuperPowerId, n.CharacterId });
            modelBuilder.Entity<CharactersSuperpowersJoin>()
                .HasOne(j => j.Character)
                .WithMany(k => k.CharactersSuperpowers)
                .HasForeignKey(i => i.CharacterId);
            modelBuilder.Entity<CharactersSuperpowersJoin>()
                .HasOne(j => j.SuperPower)
                .WithMany(k => k.CharactersSuperpowers)
                .HasForeignKey(i => i.SuperPowerId);

            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }
    }
}
