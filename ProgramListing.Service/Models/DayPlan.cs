﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class DayPlan
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string ProgramName { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string ExerciseId { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }

    public class DayPlanTableEntity : TableEntity
    {
        public DayOfWeek DayOfWeek { get; set; }
        public string ExerciseId { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}