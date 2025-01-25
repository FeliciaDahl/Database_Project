
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CostumerEntity> Costumers {  get; set; }
    
    public DbSet<ProjectManagerEntity> ProjectManagers { get; set; }

    public DbSet<ServiceEntity> Services { get; set; }

    public DbSet<StatusTypeEntity> StatusTypes { get; set; }

    public DbSet<ProjectEntity> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(x => x.StatusType)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.StatusTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
