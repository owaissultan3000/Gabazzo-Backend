using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Gabazzo_Backend.Models.DbModels
{
    public partial class gabazzodbContext : DbContext
    {
        public gabazzodbContext()
        {
        }

        public gabazzodbContext(DbContextOptions<gabazzodbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContractorPortfolio> ContractorPortfolios { get; set; }
        public virtual DbSet<ContractorService> ContractorServices { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<RegisteredContractor> RegisteredContractors { get; set; }
        public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:gabazzo-ipt-server.database.windows.net,1433;Initial Catalog=gabazzo-db;Persist Security Info=False;User ID=mugheera;Password=Gabazzo+-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ContractorPortfolio>(entity =>
            {
                entity.HasKey(e => e.PortfolioId)
                    .HasName("PK__Contract__6D3A137D27B27980");

                entity.ToTable("ContractorPortfolio");

                entity.Property(e => e.PortfolioId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Budget)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ContractorId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.Picture)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Service)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.ContractorPortfolios)
                    .HasForeignKey(d => d.ContractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contracto__Contr__756D6ECB");
            });

            modelBuilder.Entity<ContractorService>(entity =>
            {
                entity.HasKey(e => e.ServicesId)
                    .HasName("PK__Contract__BE1AC72307AABE60");

                entity.Property(e => e.ServicesId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ContractorId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(2500);

                entity.Property(e => e.EstimatedTime)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PriceFrom)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PriceTo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Service)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Contractor)
                    .WithMany(p => p.ContractorServices)
                    .HasForeignKey(d => d.ContractorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Contracto__Contr__0B5CAFEA");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.Property(e => e.ConversationId).HasMaxLength(1000);

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sender)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__Recei__2F9A1060");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.Conversations)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__Sende__2EA5EC27");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ConversationId)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("ConversationID");

                entity.Property(e => e.ReceiverId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Texts).IsUnicode(false);

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__Convers__345EC57D");
            });

            modelBuilder.Entity<RegisteredContractor>(entity =>
            {
                entity.HasKey(e => e.ContractorId)
                    .HasName("PK__Register__E964EB7D0504351F");

                entity.Property(e => e.ContractorId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyAddress)
                    .IsRequired()
                    .HasMaxLength(600);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(70);

                entity.Property(e => e.Logo).HasMaxLength(1500);

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
                    .HasName("PK__Register__1788CC4CA7D6431E");

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

            modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
