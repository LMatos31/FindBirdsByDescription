using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FindBirdsByDescription
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["Name"];
            string colour = req.Query["Colour"];
            string description = req.Query["Description"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;
            colour = colour ?? data?.colour;
            description = description ?? data?.description;
            string responseMessage = "";
            if (name ==null || colour==null || description ==null)
            {
                responseMessage = "Error 404, Please data into all fields!";
            }
            else
            {
                Birds bird = new Birds();
                bird.Name = name;
                bird.Colour = colour;
                bird.Desciprtion = description;
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
