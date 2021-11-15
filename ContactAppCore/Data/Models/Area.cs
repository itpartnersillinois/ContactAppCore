using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models
{
    public enum AreaTypeEnum
    {
        NotListed, System, Campus, College, Research, Other = 9
    }

    public class Area : BaseDataItem
    {
        public Area()
        {
        }

        public Area(string title)
        {
            IsActive = false;
            Title = title;
            InternalOrder = 3;
        }

        public virtual IEnumerable<Person> Admins { get; set; }

        public bool AllowBeta { get; set; } = false;
        public bool AllowPeople { get; set; } = false;
        public AreaTypeEnum AreaType { get; set; }
        public string Audience { get; set; }
        public string ExternalUrl { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string InternalCode { get; set; }
        public string InternalNotes { get; set; }
        public bool InternalOnly { get; set; }
        public int InternalOrder { get; set; }
        public string InternalUrl { get; set; }
        public string Notes { get; set; }
        public virtual ICollection<Office> Offices { get; set; }
        public string SearchTerms { get; set; }
    }
}