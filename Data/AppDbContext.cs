using Microsoft.EntityFrameworkCore;
namespace CourseWork.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<TrackModel> Tracks { get; set; }

}
