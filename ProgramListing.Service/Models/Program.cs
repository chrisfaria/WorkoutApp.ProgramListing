using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class Program
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public string Desc { get; set; }

        // Duration
        public int Weeks { get; set; }
        public int DaysPerWeek { get; set; }
        public int MinsPerDay { get; set; }

        // Weekly workout plan (same every week)
        public WeekPlan WeeklyPlan { get; set; }

        // Increase on update
        public int version { get; set; } = 1;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
    }

    public class ProgramCreateModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        // Duration
        public int Weeks { get; set; }
        public int DaysPerWeek { get; set; }
        public int MinsPerDay { get; set; }

        // Weekly workout plan (same every week)
        public WeekPlan WeeklyPlan { get; set; }
    }

    // Entity to use with the database. Id is not needed because partition key is used
    // from cloud database
    public class ProgramTableEntity : TableEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        // Duration
        public int Weeks { get; set; }
        public int DaysPerWeek { get; set; }
        public int MinsPerDay { get; set; }

        // Weekly workout plan (same every week)
        public WeekPlanTableEntity WeeklyPlan { get; set; }

        // Increase on update
        public int version { get; set; } = 1;
        public DateTime CreatedTime { get; set; }
    }
}
