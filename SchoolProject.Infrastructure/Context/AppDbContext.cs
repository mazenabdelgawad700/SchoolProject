using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<Instructor> Instructor { get; set; }
    public DbSet<Ins_Subject> Ins_Subject { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    Random random = new Random();

    //    var randomDepartmentId = random.Next(1, 5);

    //    List<Student> students = new List<Student>();
    //    for (int i = 2; i <= 5000; i++)
    //    {
    //        var newStudent = new Student
    //        {
    //            StudID = i * -1,
    //            Name = $"Student - {i}",
    //            Phone = $"01165981{i}",
    //            DID = randomDepartmentId,
    //            Address = $"Address - {i}"
    //        };
    //        students.Add(newStudent);
    //    }


    //    modelBuilder.Entity<Student>().HasData(students);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentSubject>()
            .HasKey(ds => new { ds.DID, ds.SubID });

        modelBuilder.Entity<Ins_Subject>()
           .HasKey(ds => new { ds.InsId, ds.SubID });

        modelBuilder.Entity<StudentSubject>()
           .HasKey(ds => new { ds.StudID, ds.SubID });

        // Configure the one to many relationship between the instructors and supervisor
        modelBuilder.Entity<Instructor>()
            .HasOne(s => s.Supervisor)
            .WithMany(i => i.Instructors)
            .HasForeignKey(s => s.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
