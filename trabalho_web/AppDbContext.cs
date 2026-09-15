using Microsoft.EntityFrameworkCore;
using trabalho_web.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<UserEntity> Usuarios { get; set; }
}
