using System.Collections.Generic;

namespace ContactAppCore.ViewModel
{
    public class ItemList
    {
        public List<AreaOfficeItem> Areas { get; set; }
        public bool IsDeniedAccess { get; set; }
        public bool IsFullAdmin { get; set; }
        public List<AreaOfficeItem> Offices { get; set; }
    }
}