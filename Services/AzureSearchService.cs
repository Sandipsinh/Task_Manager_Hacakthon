using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace Task_Manager_Hacakthon.Services
{


    public class AzureSearchService
    {
        private readonly string _apiKey;
        private readonly string _searchEndpoint;

        public AzureSearchService(IConfiguration config)
        {

            _apiKey = config["AzureSearch:ApiKey"];
            _searchEndpoint = config["AzureSearch:Endpoint"];
        }

        public async Task<string> GetUserDataAsJsonAsync(string email)
        {
            var credential = new AzureKeyCredential(_apiKey);
            var _searchClient = new SearchClient(
                new Uri(_searchEndpoint),
                "rag-task-manager-new",
                credential
            );
            var options = new SearchOptions { Size = 1000, QueryType = SearchQueryType.Full };
            var searchResults = await _searchClient.SearchAsync<SearchDocument>(email, options);

            var docs = new List<SearchDocument>();
            List<string> retrievedDocs = new List<string>();

            await foreach (var result in searchResults.Value.GetResultsAsync())
            {
                if (result.Document.TryGetValue("chunk", out var content))
                {
                    retrievedDocs.Add(content.ToString());
                }
            }

            return string.Join("\n", retrievedDocs);
        }

        public async Task<string> GetEngagementCountAsJsonAsync()
        {
            var credential = new AzureKeyCredential(_apiKey);
            var _searchClient = new SearchClient(
                new Uri(_searchEndpoint),
                "rag-customdashboard",
                credential
            );
            var options = new SearchOptions { Size = 1000, QueryType = SearchQueryType.Full };
            var searchResults = await _searchClient.SearchAsync<SearchDocument>("*", options);

            List<string> retrievedDocs = new List<string>();

            await foreach (var result in searchResults.Value.GetResultsAsync())
            {
                if (result.Document.TryGetValue("chunk", out var content))
                {
                    retrievedDocs.Add(content.ToString());
                }
            }

            return string.Join("\n", retrievedDocs);
        }


        public async Task<string> GetefileOutcomeAsJsonAsync()
        {
            var credential = new AzureKeyCredential(_apiKey);
            var _searchClient = new SearchClient(
                new Uri(_searchEndpoint),
                "rag-efileoutcome",
                credential
            );
            var options = new SearchOptions { Size = 1000, QueryType = SearchQueryType.Full };
            var searchResults = await _searchClient.SearchAsync<SearchDocument>("*", options);

            List<string> retrievedDocs = new List<string>();

            await foreach (var result in searchResults.Value.GetResultsAsync())
            {
                if (result.Document.TryGetValue("chunk", out var content))
                {
                    retrievedDocs.Add(content.ToString());
                }
            }

            return string.Join("\n", retrievedDocs);
        }

        public async Task<string> GetMilestoneInfoAsJsonAsync()
        {
            var credential = new AzureKeyCredential(_apiKey);
            var searchClientMilestone = new SearchClient(
                new Uri(_searchEndpoint),
                "rag-milestonedaysspent",
                credential
            );
            var options = new SearchOptions { Size = 1000, QueryType = SearchQueryType.Full };
            var searchResults = await searchClientMilestone.SearchAsync<SearchDocument>("*", options);

            List<string> retrievedDocs = new List<string>();

            await foreach (var result in searchResults.Value.GetResultsAsync())
            {
                if (result.Document.TryGetValue("chunk", out var content))
                {
                    retrievedDocs.Add(content.ToString());
                }
            }

            return string.Join("\n", retrievedDocs);
        }


        public async Task<string> GetCraStatusInfoAsJsonAsync()
        {
            var credential = new AzureKeyCredential(_apiKey);
            var searchClientMilestone = new SearchClient(
                new Uri(_searchEndpoint),
                "rag-crastatus",
                credential
            );
            var options = new SearchOptions { Size = 1000, QueryType = SearchQueryType.Full };
            var searchResults = await searchClientMilestone.SearchAsync<SearchDocument>("*", options);

            List<string> retrievedDocs = new List<string>();

            await foreach (var result in searchResults.Value.GetResultsAsync())
            {
                if (result.Document.TryGetValue("chunk", out var content))
                {
                    retrievedDocs.Add(content.ToString());
                }
            }

            return string.Join("\n", retrievedDocs);
        }
    }
}