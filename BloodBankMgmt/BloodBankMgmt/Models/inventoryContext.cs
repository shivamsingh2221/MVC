using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BloodBankMgmt.Models
{
    public partial class inventoryContext : DbContext
    {
        public inventoryContext()
        {
        }

        public inventoryContext(DbContextOptions<inventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Donor> Donor { get; set; }
        public virtual DbSet<Receiver> Receiver { get; set; }
        public virtual DbSet<TotalBlood> TotalBlood { get; set; }
        public virtual DbSet<Userpass> Userpass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-1GITGVV;Database=inventory;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donor>(entity =>
            {
                entity.ToTable("donor");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Receiver>(entity =>
            {
                entity.ToTable("receiver");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroup)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TotalBlood>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.DonorId).HasColumnName("Donor_ID");

                entity.Property(e => e.ReceiverId).HasColumnName("Receiver_ID");

                entity.HasOne(d => d.Donor)
                    .WithMany()
                    .HasForeignKey(d => d.DonorId)
                    .HasConstraintName("FK__TotalBloo__Donor__2D27B809");

                entity.HasOne(d => d.Receiver)
                    .WithMany()
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__TotalBloo__Recei__2E1BDC42");
            });

            modelBuilder.Entity<Userpass>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("userpass");

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Usern)
                    .HasColumnName("usern")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
