using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class WeekPlan
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public List<DayPlan> DayPlans { get; set; }
    }

    public class DayPlan
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public List<SetGroup> SetGroups { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }

    public class WeekPlanTableEntity : TableEntity
    {
        public List<DayPlanTableEntity> DayPlans { get; set; }
    }

    public class DayPlanTableEntity : TableEntity
    {
        public List<SetGroupTableEntity> SetGroups { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }


}
