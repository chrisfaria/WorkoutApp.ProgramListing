﻿using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public class MuscleGroup
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class MuscleGroupUpdateModel
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class MuscleGroupTableEntity : TableEntity
    {
        public string Desc { get; set; }
    }
}
