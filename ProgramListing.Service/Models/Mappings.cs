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
                PartitionKey = "PROGRAMS",
                RowKey = program.Id,
                Name = program.Name,
                Desc = program.Desc,
                Weeks = program.Weeks,
                DaysPerWeek = program.DaysPerWeek,
                MinsPerDay = program.MinsPerDay,
                WeeklyPlan = program.WeeklyPlan.ToTableEntity(),
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
                WeeklyPlan = program.WeeklyPlan.ToWeekPlan(),
                version = program.version,
                CreatedTime = program.CreatedTime
            };
        }

        public static WeekPlanTableEntity ToTableEntity(this WeekPlan weekplan)
        {
            return new WeekPlanTableEntity()
            {
                PartitionKey = "WEEKPLANS",
                RowKey = weekplan.Id,
                DayPlans = weekplan.DayPlans.ToTableEntity()
            };
        }

        public static WeekPlan ToWeekPlan(this WeekPlanTableEntity weekplan)
        {
            return new WeekPlan()
            {
                Id = weekplan.RowKey,
                DayPlans = weekplan.DayPlans.ToDayPlan()
            };
        }

        public static List<DayPlanTableEntity> ToTableEntity(this List<DayPlan> dayplans)
        {
            List<DayPlanTableEntity> output = new List<DayPlanTableEntity>();
            foreach (DayPlan dayplan in dayplans)
            {
                DayPlanTableEntity entity = new DayPlanTableEntity()
                {
                    PartitionKey = "DAYPLANS",
                    RowKey = dayplan.Id,
                    SetGroups = dayplan.SetGroups.ToTableEntity(),
                    DayOfWeek = dayplan.DayOfWeek
                };
                output.Add(entity);
            }

            return output;
        }

        public static List<DayPlan> ToDayPlan(this List<DayPlanTableEntity> dayplans)
        {
            List<DayPlan> output = new List<DayPlan>();
            foreach (DayPlanTableEntity dayplan in dayplans)
            {
                DayPlan entity = new DayPlan()
                {
                    Id = dayplan.RowKey,
                    SetGroups = dayplan.SetGroups.ToSetGroup(),
                    DayOfWeek = dayplan.DayOfWeek
                };
                output.Add(entity);
            }

            return output;
        }


        public static List<SetGroupTableEntity> ToTableEntity(this List<SetGroup> setgroups)
        {
            List<SetGroupTableEntity> output = new List<SetGroupTableEntity>();
            foreach (SetGroup setgroup in setgroups)
            {
                SetGroupTableEntity entity = new SetGroupTableEntity()
                {
                    PartitionKey = "SETGROUPS",
                    RowKey = setgroup.Id,
                    Exercise = setgroup.Exercise.ToTableEntity(),
                    Reps = setgroup.Reps,
                    Sets = setgroup.Sets
                };
                output.Add(entity);
            }

            return output;
        }

        public static List<SetGroup> ToSetGroup(this List<SetGroupTableEntity> setgroups)
        {
            List<SetGroup> output = new List<SetGroup>();
            foreach (SetGroupTableEntity setgroup in setgroups)
            {
                SetGroup entity = new SetGroup()
                {
                    Id = setgroup.RowKey,
                    Exercise = setgroup.Exercise.ToExercise(),
                    Reps = setgroup.Reps,
                    Sets = setgroup.Sets
                };
                output.Add(entity);
            }

            return output;
        }

        public static ExerciseTableEntity ToTableEntity(this Exercise exercise)
        {
            return new ExerciseTableEntity()
            {
                PartitionKey = "EXERCISES",
                RowKey = exercise.Id,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Desc = exercise.Desc
            };
        }

        public static Exercise ToExercise(this ExerciseTableEntity exercise)
        {
            return new Exercise()
            {
                Id = exercise.RowKey,
                Name = exercise.Name,
                MuscleGroup = exercise.MuscleGroup,
                Desc = exercise.Desc
            };
        }
    }
}
