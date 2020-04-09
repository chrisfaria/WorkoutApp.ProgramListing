using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class SetGroup
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Exercise Exercise { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }

    public class SetGroupTableEntity : TableEntity
    {
        public ExerciseTableEntity Exercise { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
    }
}
