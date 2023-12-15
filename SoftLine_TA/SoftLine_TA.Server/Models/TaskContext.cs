using Microsoft.EntityFrameworkCore;

namespace SoftLine_TA.Models
{
    public class TaskContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>()
                .HasKey(s => s.Status_ID);
            
            modelBuilder.Entity<Task>()
                .HasKey(t => t.ID);
            modelBuilder.Entity<Task>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.Status_ID);
            
            modelBuilder.Entity<Status>().HasData(
                    new Status { Status_ID = 1, Status_name = "Создана" },
                    new Status { Status_ID = 2, Status_name = "В работе" },
                    new Status { Status_ID = 3, Status_name = "Завершена" }
            );
        }
    }
}
