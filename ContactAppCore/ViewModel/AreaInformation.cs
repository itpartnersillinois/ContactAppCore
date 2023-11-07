using System.Collections.Generic;
using System.Linq;
using ContactAppCore.Data.Models;

namespace ContactAppCore.ViewModel {

    public class AreaInformation {

        public AreaInformation(Area area, bool useInternal, string search, OfficeTypeEnum? officeType) {
            AreaType = area.AreaType == AreaTypeEnum.NotListed ? "" : area.AreaType.ToString();
            Audience = area.Audience;
            ExternalUrl = area.ExternalUrl;
            Id = area.Id;
            InternalUrl = area.InternalUrl;
            Notes = area.Notes;
            Priority = area.InternalOrder;
            Title = area.Title;
            var officeGroup = area.Offices == null ? new List<Office>() : area.Offices.Where(o => o.IsActive);

            if (!useInternal) {
                officeGroup = officeGroup.Where(o => !o.InternalOnly);
            }
            if (officeType != null) {
                officeGroup = officeGroup.Where(o => o.OfficeType == officeType);
            }
            if (!string.IsNullOrEmpty(search)) {
                if (officeGroup.Any(o => (o.Title ?? "").Contains(search) || (o.SearchTerms ?? "").Contains(search) || (o.Audience ?? "").Contains(search))) {
                    officeGroup = officeGroup.Where(o => (o.Title ?? "").Contains(search) || (o.SearchTerms ?? "").Contains(search) || (o.Audience ?? "").Contains(search));
                }
            }

            Offices = officeGroup.OrderBy(o => o.InternalOrder).ThenBy(o => o.Title).Select(o => new OfficeInformation(o)).ToList();
        }

        public string AreaType { get; set; }

        public string Audience { get; set; }

        public string ExternalUrl { get; set; }

        public int Id { get; set; }

        public string InternalUrl { get; set; }

        public string Notes { get; set; }

        public IEnumerable<OfficeInformation> Offices { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
    }
}