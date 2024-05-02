using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteSPA.API.Model;

namespace TesteSPA.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TaskModel>? Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>().ToTable("Tarefas");
            modelBuilder.Entity<TaskModel>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<TaskModel>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<TaskModel>()
                .Property(task => task.dateTime)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}