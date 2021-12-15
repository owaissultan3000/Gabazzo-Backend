using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class GabazzoDBContext : DbContext
    {
        public GabazzoDBContext()
        {
        }

        public GabazzoDBContext(DbContextOptions<GabazzoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RegisteredContractor> RegisteredContractors { get; set; }
        public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=GabazzoDB;UID=sa;PWD=dingdong");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<RegisteredContractor>(entity =>
            {
                entity.HasKey(e => e.ContractorId)
                    .HasName("PK__Register__E964EB7D9EC00927");

                entity.Property(e => e.ContractorId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyAddress)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<RegisteredUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Register__1788CC4C2C047EEA");

                entity.Property(e => e.UserId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
