using System.Threading.Tasks;
using System.Web.Http;
using Dare.Web.Services.YouTubeVideo;
using Umbraco.Web.WebApi;

namespace Dare.Web.Controllers
{
    public class YouTubeVideoApiController :UmbracoApiController
    {
        private readonly IYouTubeVideoService _youTubeVideoService;
         
        public YouTubeVideoApiController(IYouTubeVideoService youTubeVideoService)
        {
            _youTubeVideoService = youTubeVideoService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetYouTubeVideos(string searchText, string pageId)
        {
            try
            {
                var id = int.Parse(pageId);
                var result = await _youTubeVideoService.GetVideos(searchText, id);

                return Ok(result);
            }
            catch
            {
                return BadRequest("Something went wrong");
            }
            
        }
    }
}