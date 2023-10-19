﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models {

    public class EmployeeProfile : BaseDataItem {
        public string Biography { get; set; }
        public string CVUrl { get; set; }
        public virtual ICollection<EmployeeActivity> EmployeeActivities { get; set; }
        public virtual ICollection<EmployeeLink> EmployeeLinks { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public bool? IsPhoneHidden { get; set; }
        public virtual ICollection<JobProfile> Jobs { get; set; }

        [NotMapped]
        public string ListedName => string.IsNullOrEmpty(ListedNameLast) || string.IsNullOrEmpty(ListedNameFirst) ? "" : ListedNameLast + ", " + ListedNameFirst;

        public string ListedNameFirst { get; set; }

        public string ListedNameLast { get; set; }

        public string OfficeInformation { get; set; }

        public string Phone { get; set; }

        public string PhotoUrl { get; set; }

        public string PreferredName { get; set; }

        public string PreferredNameLast { get; set; }

        public string PreferredPronouns { get; set; }

        public int? PrimaryProfile { get; set; }

        public string GenerateSignatureName() {
            if (string.IsNullOrWhiteSpace(PreferredName) && string.IsNullOrWhiteSpace(ListedNameFirst) && string.IsNullOrWhiteSpace(PreferredNameLast) && string.IsNullOrWhiteSpace(ListedNameLast)) {
                return "";
            }
            var name = !string.IsNullOrWhiteSpace(PreferredName) ? PreferredName : !string.IsNullOrWhiteSpace(ListedNameFirst) ? ListedNameFirst : "";
            name += " " + (!string.IsNullOrWhiteSpace(PreferredNameLast) ? PreferredNameLast : !string.IsNullOrWhiteSpace(ListedNameLast) ? ListedNameLast : "");
            if (!string.IsNullOrWhiteSpace(PreferredPronouns)) {
                name += $" ({PreferredPronouns})";
            }
            return name.Trim();
        }
    }
}