using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task_Manager_Hacakthon.Modal;
using Task_Manager_Hacakthon.Services;

namespace Task_Manager_Hacakthon.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AzureOpenAIService _openAIService;
        private readonly AzureSearchService _searchService;

        public DashboardController(AzureOpenAIService openAIService, AzureSearchService searchService)
        {

            _openAIService = openAIService;
            _searchService = searchService;

        }

        [HttpGet("getEngagementCount/{taxyear}")]
        public async Task<IActionResult> GetEngagementsCount(int taxyear)
        {
            DashboardInfo dashboardInfo = new DashboardInfo();
            var dataJson = await _searchService.GetEngagementCountAsJsonAsync();
            var totalCount = await _openAIService.GenerateEngagementCountAsync(dataJson, taxyear);
            var totalCountJson = JsonConvert.DeserializeObject<EngagementsCount>(totalCount);

            var milestoneDataJson = await _searchService.GetMilestoneInfoAsJsonAsync();
            var MilestoneInfo = await _openAIService.GenerateMilestoneInfoAsync(milestoneDataJson, taxyear);
            var milestoneInfoJson = JsonConvert.DeserializeObject<List<MilestoneInfo>>(MilestoneInfo);

            var craDataJson = await _searchService.GetCraStatusInfoAsJsonAsync();
            var craInfo = await _openAIService.GenerateCRAStatusAsync(craDataJson, taxyear);
            var craStatusInfoJson = JsonConvert.DeserializeObject<List<CRAStatus>>(craInfo);

            var outcomeJson = await _searchService.GetefileOutcomeAsJsonAsync();
            var outcomeCount = await _openAIService.GenerateEfileOutcomeAsync(outcomeJson, taxyear);
            var efileoutcomeJson = JsonConvert.DeserializeObject<EfileOutcome>(outcomeCount);

            dashboardInfo.EngagementsCount = totalCountJson;
            dashboardInfo.MilestoneInfo = milestoneInfoJson;
            dashboardInfo.CRAStatusDetail = craStatusInfoJson;
            dashboardInfo.EfileOutcomeDetail = efileoutcomeJson;
            string jsonString = JsonConvert.SerializeObject(dashboardInfo, Formatting.Indented);
            return Ok(dashboardInfo);
        }
    }
}