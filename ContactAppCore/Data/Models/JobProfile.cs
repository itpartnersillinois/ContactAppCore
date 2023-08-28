using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models {

    public class JobProfile : BaseDataItem {
        public string Biography { get; set; }
        public string Category { get; set; }
        public string EmployeeNetId => EmployeeProfile == null ? "" : EmployeeProfile.Title;
        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public int EmployeeProfileId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int InternalOrder { get; set; }
        public virtual Office Office { get; set; }
        public int OfficeId { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<JobProfileTag> Tags { get; set; }
    }
}