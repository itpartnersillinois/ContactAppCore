using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models {

    public class Log : BaseDataItem {
        public string DateCreated => LastUpdated.ToString("f");

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string NetId { get; set; }
        public string NewData { get; set; }
        public string OldData { get; set; }
    }
}