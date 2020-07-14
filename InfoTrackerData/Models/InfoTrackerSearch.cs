using InfoTrackerData.Enums;

namespace InfoTrackerProject.Models
{
    public class InfoTrackerSearch
    {
        public string SearchUrl { get; set; }
        public string[] SearchParams { get; set; }
        public InfoProvider SearchProvider { get; set; }
    }

    
}
