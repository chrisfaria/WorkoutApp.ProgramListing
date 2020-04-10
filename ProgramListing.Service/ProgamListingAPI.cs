using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using ProgramListing.Service.Models;
using System.Linq;

namespace ProgramListing.Service
{
    public static class ProgamListingAPI
    {
        [FunctionName("CreateProgram")]
        public static async Task<IActionResult> CreateProgram(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "program")] HttpRequest req,
            [Table("programs", Connection = "AzureWebJobsStorage")] IAsyncCollector<ProgramTableEntity> programData,
            [Table("programs", Connection = "AzureWebJobsStorage")] IAsyncCollector<DayPlanTableEntity> dayPlanData,
            ILogger log)
        {
            log.LogInformation("Trigger function starting to create a new program");

            // Read the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<ProgramCreateModel>(requestBody);

            // Check if program name has been used before as it's a unique key
            // TODO

            // Insert new Program data
            var program = new Program()
            {
                //Id = input.Name,
                Name = input.Name,
                Desc = input.Desc,
                Weeks = input.Weeks,
                DaysPerWeek = input.DaysPerWeek,
                MinsPerDay = input.MinsPerDay
            };
            await programData.AddAsync(program.ToTableEntity());

            // Insert all of the Day Plan data that's associated with the Program
            foreach (var dp in input.DayPlans)
            {
                var dayplan = new DayPlan()
                {
                   ProgramName = program.Name,
                   DayOfWeek = dp.DayOfWeek,
                   ExerciseId = dp.ExerciseId,
                   Reps = dp.Reps,
                   Sets = dp.Sets
                };
                await dayPlanData.AddAsync(dayplan.ToTableEntity());
            }
            
            return new OkObjectResult(input);
        }

        [FunctionName("GetProgramHeaders")]
        public static async Task<IActionResult> GetProgramHeaders(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "programheaders")] HttpRequest req,
            [Table("programs", Connection = "AzureWebJobsStorage")] CloudTable programsTable,
            ILogger log)
        {
            log.LogInformation("Getting all program headers");

            var query = new TableQuery<ProgramTableEntity>();
            var segment = await programsTable.ExecuteQuerySegmentedAsync(query, null);

            return new OkObjectResult(segment.Select(Mappings.ToProgram));
        }
    }
}
