using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models
{
    public class EmployeeActivity : BaseDataItem
    {
        public string Description { get; set; }
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public int EmployeeProfileId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int InternalOrder { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string YearEnded { get; set; }
        public string YearStarted { get; set; }
    }
}