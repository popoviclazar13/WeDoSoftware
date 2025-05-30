using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeDoSoftware.Domain.Entities;

namespace WeDoSoftware.Infrastructure.Data
{
    public class TrainingTrackerDbContext : DbContext
    {
        public TrainingTrackerDbContext(DbContextOptions<TrainingTrackerDbContext> options)
            : base(options)
        { 
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Training> Trainings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Training>()
                .Property(t => t.ExerciseType)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
