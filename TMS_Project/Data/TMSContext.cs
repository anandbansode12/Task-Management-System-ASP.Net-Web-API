using Microsoft.EntityFrameworkCore;
using TMS_Project.Models;

namespace TMS_Project.Data
{
    public class TMSContext : DbContext
    {
        public TMSContext(DbContextOptions<TMSContext> options) : base(options) { }

        public DbSet<Task1> Task1s { get; set; }
        public DbSet<TaskNote> TaskNotes { get; set; }
        public DbSet<TaskAttachment> TaskAttachments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task1>()
                .HasOne<User>(t => t.AssignedUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedUserUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Task1>()
                .HasOne<User>(t => t.CreatorUser)
                .WithMany()
                .HasForeignKey(t => t.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<TaskNote>()
                    .HasOne(tn => tn.User)
                    .WithMany()
                    .HasForeignKey(tn => tn.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                        .Property(r => r.RoleId)
                        .ValueGeneratedOnAdd(); 

        }

    }
}
