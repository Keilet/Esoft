using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Esoftr.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Executor> Executor { get; set; }
        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Executor>()
                .Property(e => e.Grade)
                .IsUnicode(false);

            modelBuilder.Entity<Executor>()
                .HasMany(e => e.Task)
                .WithRequired(e => e.Executor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manager>()
                .HasMany(e => e.Executor)
                .WithRequired(e => e.Manager)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.WorkType)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Executor)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Manager)
                .WithRequired(e => e.User);
        }
    }
}
