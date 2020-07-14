using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using InfoTrackerData.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoTrackerController : ControllerBase
    {
        readonly string[] searchTerms = new string[]
        {
            "Online", "title", "search"
        };

        [HttpGet]
        [Route("googleresult/{provider}")]
        public async Task<string> GetGoogleResult(string provider)
        {
            string searchUri = string.Empty;
            var googleUri = "https://www.google.com/search?q=online+title+search";
            var bingUri = "https://www.bing.com/search?q=online+title+search";
            InfoProvider searchProvider;
            Enum.TryParse<InfoProvider>(provider, out searchProvider);
            if (searchProvider == InfoProvider.Bing)
            {
                searchUri = bingUri;
            }
            if (searchProvider == InfoProvider.Google)
            {
                searchUri = googleUri;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(searchUri);
            request.Referer = "https://www.localhost:44337";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StringBuilder sb = new StringBuilder();
            byte[] ResultsBuffer = new byte[8192];
            //StreamReader responseReader = new StreamReader(responseStream);
            //var content = responseReader.ReadToEnd();
            //if (!string.IsNullOrEmpty(content))
            //{
            //    JObject jObject = JObject.Parse(responseReader.ReadToEnd());
            //}
            //string tempString = null;
            //int count = 0;
            //do
            //{
            //    count = responseStream.Read(ResultsBuffer, 0, ResultsBuffer.Length);
            //    if (count != 0)
            //    {
            //        tempString = Encoding.ASCII.GetString(ResultsBuffer, 0, count);
            //        sb.Append(tempString);
            //    }
            //}

            //while (count > 0);
            //string sbb = sb.ToString();

            var result = await GetDocs(searchUri);
            return result;
        }

        private async Task<string> GetDocs(string siteUrl)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage request = await httpClient.GetAsync(siteUrl);
            cancellationToken.Token.ThrowIfCancellationRequested();

            Stream response = await request.Content.ReadAsStreamAsync();
            cancellationToken.Token.ThrowIfCancellationRequested();

            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(response);
            var documents = document.All.Take(100);
            var term = "www.infotrack.com.au".ToLower();
            List<int> positions = new List<int>();
            foreach(var (doc, index) in documents.Select((value, i) => (value, i)))
            {
                if (doc.ParentElement != null && doc.QuerySelectorAll("a").Count() > 0)
                {
                    if (doc.ParentElement.InnerHtml.Contains(term))
                    {
                        positions.Add(index);
                    }
                }
            }
            //var cdocs = documents.Where((d, index) => d.QuerySelectorAll("a").Length >= 0 && d.OuterHtml.Contains(term)).Select((doc, index) => new { Position = index }).ToList();
            //var c = documents.Where(d => d.QuerySelectorAll("a").Length >= 0 && d.ParentElement.InnerHtml.Contains(term)).ToList();
            //var matchedDocuments = documents.Where(doc => doc.ClassName == "views-field views-field-nothing" && (doc.ParentElement.OuterHtml.Contains(term)));
            return string.Join(',', positions) ?? "0";
        }

        // GET: api/InfoTracker
        [HttpGet]
        public IEnumerable<string> Get()
        {
            

            return new string[] { "value1", "value2" };
        }

        // GET: api/InfoTracker/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InfoTracker
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/InfoTracker/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
