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
            [Table("programs", Connection = "AzureWebJobsStorage")] IAsyncCollector<ProgramTableEntity> programTable,
            ILogger log)
        {
            log.LogInformation("Getting all program headers");

            // Read the body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<ProgramCreateModel>(requestBody);

            // Map the request input to a Program
            var program = new Program()
            {
                Name = input.Name,
                Desc = input.Desc,
                Weeks = input.Weeks,
                DaysPerWeek = input.DaysPerWeek,
                MinsPerDay = input.MinsPerDay,
                WeeklyPlan = input.WeeklyPlan
            };

            try
            {
                // Insert the program data into the database table
                await programTable.AddAsync(program.ToTableEntity());
            }
            catch (StorageException e) when (e.RequestInformation.HttpStatusCode == 500)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(program);
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
