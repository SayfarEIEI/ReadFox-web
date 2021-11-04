using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReadFox.Models.db_ReadFox
{
    public partial class ReadFoxContext : DbContext
    {
        public ReadFoxContext()
        {
        }

        public ReadFoxContext(DbContextOptions<ReadFoxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Typestory> Typestorys { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersBook> UsersBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-1N78MB1\\SQLEXPRESS;Database=ReadFox;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ProductName)
                    .HasName("PK_Book");

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ImageName).HasMaxLength(256);

                entity.Property(e => e.TypestoryId).HasColumnName("TypestoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Books_Categorys");

                entity.HasOne(d => d.Typestory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.TypestoryId)
                    .HasConstraintName("FK_Books_Typestorys");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Typestory>(entity =>
            {
                entity.Property(e => e.TypestoryId).HasColumnName("TypestoryID");

                entity.Property(e => e.TypestoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(100)
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<UsersBook>(entity =>
            {
                entity.Property(e => e.BookName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
