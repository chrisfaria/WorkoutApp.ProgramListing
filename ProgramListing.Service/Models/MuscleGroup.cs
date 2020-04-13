using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class MuscleGroup
    {
        public string[] MuscleGroupList { get; set; }
    }

    public class MuscleGroupTableEntity : TableEntity
    {
        public string MuscleGroupList { get; set; }
    }
}
