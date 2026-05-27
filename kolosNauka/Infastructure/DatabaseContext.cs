using kolosNauka.Entities;
using Microsoft.EntityFrameworkCore;

namespace kolosNauka.Infastructure;

public class DatabaseContext(DbContextOptions opt, IConfiguration configuration) : DbContext(opt)
{
    public virtual DbSet<PC> PCs { get; set; }
    public virtual DbSet<Components> Components { get; set; }
    public virtual DbSet<PCComponents> PCComponents { get; set; }
    public virtual DbSet<ComponentTypes> ComponentTypes { get; set; }
    public virtual DbSet<ComponentManufacturers> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema(configuration["DB:DefaultSchema"]);

        modelBuilder.Entity<PC>(opt =>
        {
            opt.HasKey(e => e.Id);
            
            opt.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            opt.Property(e => e.Weight)
                .IsRequired();

            opt.Property(e => e.CreatedAt)
                .IsRequired();

            opt.Property(e => e.Stock)
                .IsRequired();
                
        });

        modelBuilder.Entity<PCComponents>(opt =>
        {
            opt.HasKey(e => new { e.PCId, e.ComponentCode });
            opt.Property(e => e.Amount)
                .IsRequired();

            opt.HasOne(e => e.PC)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.PCId);

            opt.HasOne(e => e.Component)
                .WithMany(e => e.PCComponents)
                .HasForeignKey(e => e.ComponentCode);

        });

        modelBuilder.Entity<ComponentTypes>(opt =>
        {
            opt.HasKey(e => e.Id);
            opt.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);
            opt.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

        });

        modelBuilder.Entity<ComponentManufacturers>(opt =>
        {
            opt.HasKey(e => e.Id);
            
            opt.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(30);
            
            opt.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(300);

            opt.Property(e => e.FoundationDate)
                .IsRequired();
        });

        modelBuilder.Entity<Components>(opt =>
        {
            opt.HasKey(e => e.Code);
            
            opt.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(300);

            opt.Property(e => e.Description)
                .IsRequired();

            opt.HasOne(e => e.ComponentTypes)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentTypesId);
            
            opt.HasOne(e => e.ComponentManufacturers)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ComponentManufacturerId);
            
        });
        
    }
}