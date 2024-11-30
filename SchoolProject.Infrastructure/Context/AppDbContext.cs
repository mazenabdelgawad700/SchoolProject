using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Entities.Identity;
namespace SchoolProject.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<Instructor> Instructor { get; set; }
    public DbSet<Ins_Subject> Ins_Subject { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DepartmentSubject>()
            .HasKey(ds => new { ds.DID, ds.SubID });

        modelBuilder.Entity<Ins_Subject>()
           .HasKey(ds => new { ds.InsId, ds.SubID });

        modelBuilder.Entity<StudentSubject>()
           .HasKey(ds => new { ds.StudID, ds.SubID });

        modelBuilder.Entity<Instructor>()
            .HasOne(s => s.Supervisor)
            .WithMany(i => i.Instructors)
            .HasForeignKey(s => s.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}