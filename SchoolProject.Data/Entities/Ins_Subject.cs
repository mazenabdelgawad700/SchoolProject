using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Domain.Entities
{
    public class Ins_Subject
    {
        public int InsId { get; set; }
        public int SubID { get; set; }

        [ForeignKey("InsId")]
        [InverseProperty("Ins_Subjects")]
        public virtual Instructor? Instructor { get; set; } = null!;

        [ForeignKey("SubID")]
        [InverseProperty("Ins_Subjects")]
        public virtual Subjects? Subjects { get; set; } = null!;
    }
}
