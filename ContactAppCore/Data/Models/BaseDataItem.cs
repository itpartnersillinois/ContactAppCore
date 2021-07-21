using System;

namespace ContactAppCore.Data.Models
{
    public abstract class BaseDataItem
    {
        public abstract int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Title { get; set; }
    }
}