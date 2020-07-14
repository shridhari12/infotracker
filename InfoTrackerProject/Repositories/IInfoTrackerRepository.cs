using System.Threading.Tasks;

namespace InfoTrackerAPI.Repositories
{
    public interface IInfoTrackerRepository
    {
        Task<string> GetSearchResultsFromGoogle(string url);
    }
}
