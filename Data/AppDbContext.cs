using Microsoft.EntityFrameworkCore;

namespace KPCourseWork.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RoleModel> Roles { get; set; }
    public DbSet<TrackModel> Tracks { get; set; }
    public DbSet<PlaylistModel> Playlists { get; set; }
    public DbSet<GenreModel> Genre { get; set; }
    public DbSet<FavoriteModel> Favorite { get; set; }
    public DbSet<SubscriptionModel> Subscriptions { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<SubscriptionModel>()
    //         .HasKey(s => new { s.SubscriberId, s.SubscribedToId });
    //
    //     modelBuilder.Entity<SubscriptionModel>()
    //         .HasOne(s => s.Subscriber)
    //         .WithMany(u => u.Subscriptions)
    //         .HasForeignKey(s => s.SubscriberId)
    //         .OnDelete(DeleteBehavior.Restrict);
    //
    //     modelBuilder.Entity<SubscriptionModel>()
    //         .HasOne(s => s.SubscribedTo)
    //         .WithMany(u => u.Subscribers)
    //         .HasForeignKey(s => s.SubscribedToId)
    //         .OnDelete(DeleteBehavior.Restrict);
    // }
    
}