using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public static class Mappings
    {
        public static ProgramTableEntity ToTableEntity(this Program program)
        {
            return new ProgramTableEntity()
            {
                PartitionKey = program.Name,
                RowKey = program.Id,
                Name = program.Name,
                Desc = program.Desc,
                Weeks = program.Weeks,
                DaysPerWeek = program.DaysPerWeek,
                MinsPerDay = program.MinsPerDay,
                //DayPlan = program.DayPlan.ToTableEntity(),
                version = program.version,
                CreatedTime = program.CreatedTime
            };
        }

        public static Program ToProgram(this ProgramTableEntity program)
        {
            return new Program()
            {
                Id = program.RowKey,
                Name = program.Name,
                Desc = program.Desc,
                Weeks = program.Weeks,
                DaysPerWeek = program.DaysPerWeek,
                MinsPerDay = program.MinsPerDay,
                //DayPlan = program.DayPlan.ToDayPlan(),
                version = program.version,
                CreatedTime = program.CreatedTime
            };
        }

        public static DayPlanTableEntity ToTableEntity(this DayPlan exercise)
        {
            return new DayPlanTableEntity()
            {
                PartitionKey = exercise.ProgramName,
                RowKey = exercise.Id,
                DayOfWeek = exercise.DayOfWeek,
                ExerciseId = exercise.ExerciseId,
                Reps = exercise.Reps,
                Sets = exercise.Sets
            };
        }

        public static DayPlan ToDayPlan(this DayPlanTableEntity exercise)
        {
            return new DayPlan()
            {
                Id = exercise.RowKey,
                DayOfWeek = exercise.DayOfWeek,
                ExerciseId = exercise.ExerciseId,
                Reps = exercise.Reps,
                Sets = exercise.Sets
            };
        }
    }
}
