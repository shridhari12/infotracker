using System.Net.Http;

namespace InfoTrackerRepository.Repository
{
    public class InfoTrackerRepository : IInfoTrackerRepository
    {
        public readonly string googleSearchApiKey = "<some-api-key>";
        public readonly string googleSearchUrl = $"https://www.googleapis.com/customsearch/v1";
        // https://www.googleapis.com/customsearch/v1?key=&cx=017576662512468239146:omuauf_lfve&q=lectures
        public void GetSearchResultsFromGoogle(string url)
        {
            using (var httpClient = new HttpClient())
            {
                string requestUri = $"{googleSearchUrl}?key={googleSearchApiKey}&cx={url}";
                var response = httpClient.GetAsync(requestUri);
            }
        }
    }
}
