using Task_Manager_Hacakthon.Services;
using Microsoft.AspNetCore.Mvc;
using Task_Manager_Hacakthon.Modal;
using Newtonsoft.Json;

namespace Task_Manager_Hacakthon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly AzureOpenAIService _openAIService;
        private readonly AzureSearchService _searchService;

        public TaskController(AzureOpenAIService openAIService, AzureSearchService searchService)
        {

            _openAIService = openAIService;
            _searchService = searchService;

        }

        [HttpGet("getTasks/{email}/{prompt}")]
        public async Task<IActionResult> GetTasks(string email, string prompt)
        {
            var dataJson = await _searchService.GetUserDataAsJsonAsync(email);
            var tasksJson = await _openAIService.GenerateTasksAsync(dataJson, email, prompt);
            var taskInfoJson = JsonConvert.DeserializeObject<List<TaskItem>>(tasksJson);
            return Ok(taskInfoJson);
        }


    }


}