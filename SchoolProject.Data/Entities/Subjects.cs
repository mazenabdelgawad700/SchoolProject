using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolProject.Domain.Entities;
public class Subjects
{
    public Subjects()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmentSubjects = new HashSet<DepartmentSubject>();
        Ins_Subjects = new HashSet<Ins_Subject>();
    }
    [Key]
    public int SubID { get; set; }
    [StringLength(200)]
    public string? SubjectName { get; set; }
    public int? Period { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

    [InverseProperty("Subjects")]
    public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
}
