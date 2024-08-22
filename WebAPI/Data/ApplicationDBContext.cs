using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Entities;

namespace WebAPI.Data
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Subject> subjects { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentSubject>(entity =>
            {
                entity.HasKey(bp => new { bp.SubjectId, bp.StudentId });

                entity.HasOne(bp => bp.Student)
                .WithMany(b => b.StudentSubjects)
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(bp => bp.Subject)
                .WithMany(b => b.StudentSubjects)
                .HasForeignKey(b => b.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
