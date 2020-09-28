using System.Collections.Generic;
using System.Threading.Tasks;
using Dare.Web.Services.YouTubeVideo.Models;

namespace Dare.Web.Services.YouTubeVideo
{
    public interface IYouTubeVideoService 
    { 
        Task<List<YouTubeVideoModel>> GetVideos(string searchText, int pageId, long maxNoOfItems = 12);

        void SaveVideos(List<YouTubeVideoModel> videos, int pageId);  
    }
}
