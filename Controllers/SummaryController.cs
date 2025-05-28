using Microsoft.AspNetCore.Mvc;
using Task_Manager_Hacakthon.Services;

namespace Kili_Summary_hacakthon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        //private readonly AzureAISearchService _searchService;
        private readonly AzureOpenAIService _openAIService;
        //private readonly SampleAzureAiService _sampleAIService;

        public SummaryController(AzureOpenAIService openAIService)
        {

            _openAIService = openAIService;

        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateSummary([FromBody] SummaryList request)
        {
            // Step 2: Generate summary using Azure OpenAI (GPT-4o)
            // var summary = await _openAIService.GenerateSummaryAsync(request);

            return Ok();
        }
    }


}
