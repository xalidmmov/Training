using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.Entities;

namespace Training.DAL.DAL
{
    public class TrainingDbContext : DbContext
    {
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public TrainingDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>().HasOne(x => x.Category).WithMany(x => x.Trainers);
            modelBuilder.Entity<Category>().HasMany(x => x.Trainers).WithOne(x => x.Category);
            modelBuilder.Entity<Trainer>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Trainer>().Property(x => x.Surname).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Category>().Property(x => x.CName).IsRequired().HasMaxLength(50);
            base.OnModelCreating(modelBuilder);
        }
    }
}
