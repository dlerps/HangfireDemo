using Microsoft.AspNetCore.Mvc;

namespace HangfireDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateJob()
    {
        //var jobId = backgroundJobClient.Enqueue(() => Console.WriteLine("Hello Hangfire"));
        return Accepted("11");
    }
}