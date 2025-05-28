using Task_Manager_Hacakthon.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task_Manager_Hacakthon.Modal;

namespace Task_Manager_Hacakthon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase    {
       
        private readonly AzureOpenAIService _openAIService;
        private readonly AzureSearchService _searchService;

        public TaskController( AzureOpenAIService openAIService)
        {
           
            _openAIService = openAIService;
           
        }

        //[HttpPost("generate")]
        //public async Task<IActionResult> GenerateSummary([FromBody] SummaryList request)
        //{
        //    // Step 2: Generate summary using Azure OpenAI (GPT-4o)
        //    var summary = await _openAIService.GenerateSummaryAsync(request);

        //    return Ok(new { Summary = summary });
        //}

        [HttpGet("getTasks/{userId}")]
        public async Task<IActionResult> GetTasks(string userId)
        {
            var dataJson = await _searchService.GetUserDataAsJsonAsync(userId);
            var tasksJson = await _openAIService.GenerateTasksAsync(dataJson);
            return Ok(tasksJson);
        }

        [HttpPost("combine")]
        public async Task<IActionResult> CombineTasks([FromBody] UserTaskRequest request)
        {
            var aiDataJson = await _searchService.GetUserDataAsJsonAsync(request.UserId);
            var aiTasksJson = await _openAIService.GenerateTasksAsync(aiDataJson);
            var refinedJson = await _openAIService.RefineCombinedTasksAsync(aiTasksJson, request.UserTasks);
            return Ok(refinedJson);
        }
    }

   
}
