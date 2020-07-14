using System.Net.Http;
using System.Threading.Tasks;

namespace InfoTrackerAPI.Repositories
{
    public class InfoTrackerRepository : IInfoTrackerRepository
    {
        public readonly string getSearchResultFromApiUrl = "https://localhost:44369/api/infotracker/googleresult/sdsdf"; //50001
        public async Task<string> GetSearchResultsFromGoogle(string url)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                response = await httpClient.GetAsync(getSearchResultFromApiUrl);
            }
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
