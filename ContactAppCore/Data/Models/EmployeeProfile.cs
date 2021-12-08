using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models
{
    public class EmployeeProfile : BaseDataItem
    {
        public string Biography { get; set; }

        public string CVUrl { get; set; }

        public virtual ICollection<EmployeeActivity> EmployeeActivities { get; set; }
        public virtual ICollection<EmployeeLink> EmployeeLinks { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public virtual ICollection<JobProfile> Jobs { get; set; }
        public string OfficeInformation { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string PreferredName { get; set; }
        public int? PrimaryProfile { get; set; }
    }
}