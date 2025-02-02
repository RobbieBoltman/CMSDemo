using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockManagementAPI.Models;

public partial class CmsdemoContext : DbContext
{
    public CmsdemoContext()
    {
    }

    public CmsdemoContext(DbContextOptions<CmsdemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<StockAccessory> StockAccessories { get; set; }

    public virtual DbSet<StockItem> StockItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Robbie-PC;Initial Catalog=CMSDemo;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.StockItem).WithMany(p => p.Images)
                .HasForeignKey(d => d.StockItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_StockItems");
        });

        modelBuilder.Entity<StockAccessory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.StockItem).WithMany(p => p.StockAccessories)
                .HasForeignKey(d => d.StockItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockAccessories_StockAccessories");
        });

        modelBuilder.Entity<StockItem>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Colour)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Dtcreated)
                .HasColumnType("datetime")
                .HasColumnName("DTCreated");
            entity.Property(e => e.Dtupdated)
                .HasColumnType("datetime")
                .HasColumnName("DTUpdated");
            entity.Property(e => e.Kms).HasColumnName("KMS");
            entity.Property(e => e.Make)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RegNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RetailPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Vin)
                .HasMaxLength(17)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("VIN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
