﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactAppCore.Data.Models {

    public class Person : BaseDataItem {

        public Person() {
        }

        public Person(string name, int? areaId = null, int? officeId = null, bool fullOfficeAdmin = false) {
            name = name.ToLowerInvariant();
            this.Title = name.EndsWith("@illinois.edu") ? name : name + "@illinois.edu";
            this.IsFullAdmin = areaId == null && officeId == null;
            this.IsActive = true;
            this.AreaId = areaId;
            this.OfficeId = officeId;
            this.CanEditAllPeopleInUnit = fullOfficeAdmin;
        }

        public int? AreaId { get; set; }
        public bool CanEditAllPeopleInUnit { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public bool IsFullAdmin { get; set; }
        public int? OfficeId { get; set; }
    }
}