using InfoTrackerAPI.Repositories;
using InfoTrackerProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfoTrackerProject.Controllers
{
    [Route("search")]
    public class SearchController : Controller
    {
        private readonly IInfoTrackerRepository _infoTrackerRepository;

        public SearchController(IInfoTrackerRepository infoTrackerRepository)
        {
            _infoTrackerRepository = infoTrackerRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Search")]
        public async Task<IActionResult> Search(InfoTrackerSearch infoTrackerSearch)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Search input not provided");
            }

            var searchUrl = infoTrackerSearch?.SearchProvider ?? null;

            var searchResult = await _infoTrackerRepository.GetSearchResultsFromGoogle(searchUrl.ToString());
            var viewModel = new InfoTrackerSearchResultModel
            {
                SearchUrl = searchUrl.ToString(),
                SearchResult = searchResult
            };

            return View(viewModel);

        }

    }
}
