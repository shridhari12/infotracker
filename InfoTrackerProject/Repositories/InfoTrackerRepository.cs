using System.Net.Http;
using System.Threading.Tasks;

namespace InfoTrackerAPI.Repositories
{
    public class InfoTrackerRepository : IInfoTrackerRepository
    {
        public async Task<string> GetSearchResultsFromGoogle(string infoProvider)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                string searchApiUrl = string.Join("", "https://localhost:44369/api/infotracker/googleresult/", infoProvider);
                response = await httpClient.GetAsync(searchApiUrl);
            }
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
