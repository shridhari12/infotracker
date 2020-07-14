using AngleSharp;

namespace InfoTrackerAPI.Helpers
{
    public class HtmlScraper
    {
        readonly IBrowsingContext _browsingContext;
        public HtmlScraper(IBrowsingContext browsingContext)
        {
            _browsingContext = browsingContext;
        }


    }
}
