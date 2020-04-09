using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class Exercise
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string Desc { get; set; }

        // CAN USE IF YOU HAVE A CREATE API AND DB TABLE FOR EXERCISE DATA
        /// Increase on update
        //public int version { get; set; } = 1;
        //public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        //public DateTime LastUpdatedTime { get; set; }
    }

    public class ExerciseTableEntity : TableEntity
    {
        public string Name { get; set; }
        public string MuscleGroup { get; set; }
        public string Desc { get; set; }

        // CAN USE IF YOU HAVE A CREATE API AND DB TABLE FOR EXERCISE DATA
        /// Increase on update
        //public int version { get; set; } = 1;
        //public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        //public DateTime LastUpdatedTime { get; set; }
    }
}
