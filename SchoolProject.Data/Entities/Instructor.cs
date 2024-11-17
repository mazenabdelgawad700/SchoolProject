using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities
{
    public class Instructor
    {
        public Instructor()
        {
            Instructors = new HashSet<Instructor>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        public int InsId { get; set; }
        public string? InstructorName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Position { get; set; } = string.Empty;
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int? DID { get; set; }

        [ForeignKey("DID")]
        [InverseProperty("Instructors")]
        public Department? Department { get; set; } = null!;


        [InverseProperty("Instructor")]
        public Department? DepartmentManager { get; set; } = null!;

        [ForeignKey("SupervisorId")]
        [InverseProperty("Instructors")]
        public virtual Instructor? Supervisor { get; set; } = null!;

        [InverseProperty("Supervisor")]
        public virtual ICollection<Instructor> Instructors { get; set; }

        [InverseProperty("Instructor")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
