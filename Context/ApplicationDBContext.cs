using backendServer.Models;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public DbSet<UserModel> Users { get; set; } //models or entities
    public DbSet<ListasModel> Listas { get; set; }
    public DbSet<ActividadesModel> Actividades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>().HasKey(u => u.IdUser);
        modelBuilder.Entity<ListasModel>().HasKey(lista => lista.IdLista);
        modelBuilder.Entity<ActividadesModel>().HasKey(act => act.IdActividad);
    }
}