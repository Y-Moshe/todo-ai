using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AppDb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
