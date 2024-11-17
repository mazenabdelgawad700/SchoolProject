using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolProject.Domain.Entities;

public class Department
{
    public Department()
    {
        Students = new HashSet<Student>();
        DepartmentSubjects = new HashSet<DepartmentSubject>();
        Instructors = new HashSet<Instructor>();
    }
    [Key]
    public int DID { get; set; }
    [StringLength(200)]
    public string? DName { get; set; }
    public int? InsManager { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Student> Students { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Instructor> Instructors { get; set; }

    [ForeignKey("InsManager")]
    [InverseProperty("DepartmentManager")]
    public virtual Instructor? Instructor { get; set; } = null!;
}
