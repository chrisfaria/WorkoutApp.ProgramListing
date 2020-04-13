using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProgramListing.Service.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace ProgramListing.Service
{
    public static class ExerciseAPI
    {
        [FunctionName("CreateExercise")]
        public static async Task<IActionResult> CreateExercise(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "exercise")] HttpRequest req,
            [Table("exercises", Connection = "AzureWebJobsStorage")] IAsyncCollector<ExerciseTableEntity> exerciseData,
            [Table("exercises", Connection = "AzureWebJobsStorage")] CloudTable exerciseTable,
            ILogger log)
        {
            log.LogInformation("Trigger function starting to create a new exercise");

            // Read the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<ExerciseCreateModel[]>(requestBody);
            var output = new List<Exercise>();

            // Ensure valid Muscle Group for each exercise before allowing the request to be processed
            foreach (var inputExercise in input)
            {

                var findOperation = TableOperation.Retrieve<MuscleGroupTableEntity>("MUSCLE_GROUP", inputExercise.MuscleGroup);
                var findResult = await exerciseTable.ExecuteAsync(findOperation);
                if (findResult.Result == null)
                {
                    return new BadRequestObjectResult("Muscle group name not recognized");
                }

                var exercise = new Exercise()
                {
                    Name = inputExercise.Name,
                    MuscleGroup = inputExercise.MuscleGroup,
                    Desc = inputExercise.Desc
                };

                // Save this model to be inserted on success of this muscle group check
                output.Add(exercise);
            }

            foreach (var exercise in output)
            {
                await exerciseData.AddAsync(exercise.ToTableEntity());
            }

            return new OkObjectResult(output);
        }

        [FunctionName("CreateUpdateMuscleGroup")]
        public static async Task<IActionResult> UpdateMuscleGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "musclegroup")] HttpRequest req,
            [Table("exercises", Connection = "AzureWebJobsStorage")] CloudTable exerciseTable,
            ILogger log)
        {
            log.LogInformation("Trigger function starting to create or update the muscle group data");

            // Read the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<MuscleGroupUpdateModel[]>(requestBody);

            //// Find the row
            //var findOperation = TableOperation.Retrieve<MuscleGroupTableEntity>("MUSCLE_GROUP", "LIST");
            //var findResult = await exerciseTable.ExecuteAsync(findOperation);
            //if (findResult.Result == null)
            //{
            //    return new NotFoundResult();
            //}

            foreach( var inputMuscleGroup in input)
            {
                // If found then replace the data with new values
                var newMuscleGroup = new MuscleGroup()
                {
                    Name = inputMuscleGroup.Name,
                    Desc = inputMuscleGroup.Desc
                };

                // Replace the data in the database
                var replaceOperation = TableOperation.InsertOrReplace(newMuscleGroup.ToTableEntity());
                await exerciseTable.ExecuteAsync(replaceOperation);
            }

            return new OkObjectResult(input);
        }
    }
}
