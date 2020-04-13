using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramListing.Service.Models
{
    public static class Mappings
    {
        // Program mappings
        public static ProgramTableEntity ToTableEntity(this Program program)
        {
            return new ProgramTableEntity()
            {
                PartitionKey = "PROGRAM_HEADER",
                RowKey = program.Id,
                Name = program.Name,
                Desc = program.Desc,
                Weeks = program.Weeks,
                DaysPerWeek = program.DaysPerWeek,
                MinsPerDay = program.MinsPerDay,
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
                version = program.version,
                CreatedTime = program.CreatedTime
            };
        }

        // Day Plan Mapping
        public static DayPlanTableEntity ToTableEntity(this DayPlan exercise)
        {
            return new DayPlanTableEntity()
            {
                PartitionKey = "PROGRAM_DETAIL_" + exercise.ProgramId,
                RowKey = exercise.Id,
                DayOfWeek = (int)exercise.DayOfWeek,
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
                DayOfWeek = (DayOfWeek)exercise.DayOfWeek,
                ExerciseId = exercise.ExerciseId,
                Reps = exercise.Reps,
                Sets = exercise.Sets
            };
        }

        // Exercise Mapping
        public static ExerciseTableEntity ToTableEntity(this Exercise exercise)
        {
            return new ExerciseTableEntity()
            {
                PartitionKey = "EXERCISE",
                RowKey = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Desc = exercise.Desc,
                version = exercise.version,
                CreatedTime = exercise.CreatedTime
            };
        }

        public static Exercise ToExercise(this ExerciseTableEntity exercise)
        {
            return new Exercise()
            {
                Id = exercise.RowKey,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Desc = exercise.Desc,
                version = exercise.version,
                CreatedTime = exercise.CreatedTime
            };
        }

        // Mucle Group Mapping
        public static MuscleGroupTableEntity ToTableEntity(this MuscleGroup muscleGroup)
        {
            return new MuscleGroupTableEntity()
            {
                PartitionKey = "MUSCLE_GROUP",
                RowKey = muscleGroup.Name,
                Desc = muscleGroup.Desc
            };
        }

        public static MuscleGroup ToMuscleGroup(this MuscleGroupTableEntity muscleGroup)
        {
            return new MuscleGroup()
            {
                Name = muscleGroup.RowKey,
                Desc = muscleGroup.Desc
            };
        }
    }
}
