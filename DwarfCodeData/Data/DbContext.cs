using DwarfCodeData.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Anime> Animes { get; set; }
    public DbSet<User> Users { get; set; }
    //public DbSet<AnimeUser> AnimeUsers { get; set; }
    public DbSet<AnimeScore> AnimeScores { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relació AnimeScore
        modelBuilder.Entity<AnimeScore>()
            .HasKey(asc => new { asc.UserId, asc.AnimeId });

        modelBuilder.Entity<AnimeScore>()
            .HasOne(asc => asc.User)
            .WithMany(u => u.AnimeScores)
            .HasForeignKey(asc => asc.UserId);

        modelBuilder.Entity<AnimeScore>()
            .HasOne(asc => asc.Anime)
            .WithMany(a => a.AnimeScores)
            .HasForeignKey(asc => asc.AnimeId);
    }
}
