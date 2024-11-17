using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolProject.Domain.Entities;

public class Student
{

    public Student()
    {
        StudentSubject = new HashSet<StudentSubject>();
    }

    [Key]
    public int StudID { get; set; }
    [StringLength(200)]
    public string? Name { get; set; }
    [StringLength(200)]
    public string? Address { get; set; }
    [StringLength(200)]
    public string? Phone { get; set; }
    public int? DID { get; set; }

    [ForeignKey("DID")]
    public virtual Department? Department { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<StudentSubject> StudentSubject { get; set; }
}
