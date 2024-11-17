using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities;

public class DepartmentSubject
{
    public int DID { get; set; }
    public int SubID { get; set; }

    [ForeignKey("DID")]
    [InverseProperty("DepartmentSubjects")]
    public virtual Department? Department { get; set; }

    [ForeignKey("SubID")]
    [InverseProperty("DepartmentSubjects")]
    public virtual Subjects? Subject { get; set; }
}
