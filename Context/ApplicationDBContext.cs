using backendServer.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public DbSet<UserModel> Users { get; set; } //models or entities

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>().HasKey(u => u.IdUser);
    }
}