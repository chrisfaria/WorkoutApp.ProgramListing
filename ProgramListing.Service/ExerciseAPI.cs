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

namespace ProgramListing.Service
{
    public static class ExerciseAPI
    {
        [FunctionName("ExerciseAPI")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("exercises", Connection = "AzureWebJobsStorage")] IAsyncCollector<ExerciseTableEntity> exerciseData,
            ILogger log)
        {
            log.LogInformation("Trigger function starting to create a new exercise");

            // Read the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<ExerciseCreateModel>(requestBody);

            var exercise = new Exercise()
            {
                Name = input.Name,
                MuscleGroup = input.MuscleGroup,
                Desc = input.Desc
            };
            await exerciseData.AddAsync(exercise.ToTableEntity());

            return new OkObjectResult(exercise);
        }
    }
}
