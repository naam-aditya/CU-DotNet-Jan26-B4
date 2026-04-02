using System;
using System.Collections.Generic;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext() {}

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=library.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("authors");

            entity.Property(e => e.AuthorId)
                .HasColumnName("authorId");
                
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.Property(e => e.BookId)
                .HasColumnName("bookId");

            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasForeignKey(d => d.AuthorId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
