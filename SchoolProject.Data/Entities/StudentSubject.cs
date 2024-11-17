using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class StudentSubject
{
    public int StudID { get; set; }
    public int SubID { get; set; }
    public decimal? grade { get; set; }

    [ForeignKey("StudID")]
    [InverseProperty("StudentSubject")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubID")]
    [InverseProperty("StudentsSubjects")]
    public virtual Subjects? Subject { get; set; }
}
