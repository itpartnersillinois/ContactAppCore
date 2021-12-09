﻿using ContactAppCore.Data.Models;

namespace ContactAppCore.ViewModel
{
    public class EmployeeActivityInformation
    {
        public EmployeeActivityInformation()
        {
        }

        public EmployeeActivityInformation(EmployeeActivity e)
        {
            Description = e.Description;
            InternalOrder = e.InternalOrder;
            Type = e.Type;
            Url = e.Url ?? "";
            YearEnded = e.YearEnded ?? "";
            YearStarted = e.YearStarted ?? "";
        }

        public string Description { get; set; }
        public int InternalOrder { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string YearEnded { get; set; }
        public string YearStarted { get; set; }
    }
}