using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AvtoSalon.Entities
{
    public partial class group_2_is_31Context : DbContext
    {
        public group_2_is_31Context()
        {
        }

        public group_2_is_31Context(DbContextOptions<group_2_is_31Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Profession> Professions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server=s.anosov.ru;port=7078;user=group_2_is_31;password=FgCKGL;database=group_2_is_31");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.Country, "country");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CarBrand)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("car_brand")
                    .HasDefaultValueSql("''")
                    .HasComment("марка авто");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasComment("Страна");

                entity.Property(e => e.Year)
                    .HasColumnType("date")
                    .HasColumnName("year")
                    .HasComment("год");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Countries");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasIndex(e => e.Car, "FK_Car");

                entity.HasIndex(e => e.Client, "FK_Client");

                entity.HasIndex(e => e.Worker, "FK_Worker");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Car).HasColumnName("car");

                entity.Property(e => e.Client).HasColumnName("client");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Worker).HasColumnName("worker");

                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Car)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Car");

                entity.HasOne(d => d.ClientNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Client)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client");

                entity.HasOne(d => d.WorkerNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Worker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worker");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Countries)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("countries")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Profession>(entity =>
            {
                entity.ToTable("Profession");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Role, "FK_Roles");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("login")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Roles");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.HasIndex(e => e.Profession, "FK_Profession");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Profession).HasColumnName("profession");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.HasOne(d => d.ProfessionNavigation)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.Profession)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profession");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
