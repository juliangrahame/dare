using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dare.Web.Services.YouTubeVideo.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Newtonsoft.Json;
using Umbraco.Core.Services;
using Umbraco.Web.PublishedModels;

namespace Dare.Web.Services.YouTubeVideo
{
    public class YouTubeVideoService : IYouTubeVideoService
    {
        string ApiKey = ConfigurationManager.AppSettings["YouTubeApiKey"];
        // const string ApiKey = "AIzaSyBfCBpUlHmhOjWVUObj08Km93jod-1mGc8";

        private readonly IContentService _contentService;
        private readonly YouTubeService _youTubeVideoService;

        public YouTubeVideoService(IContentService contentService)
        {
            _contentService = contentService;
            _youTubeVideoService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey,
                ApplicationName = this.GetType().ToString()
            }); 
        }

        public async Task<List<YouTubeVideoModel>> GetVideos(string searchText, int pageId, long maxNoOfItems = 12)
        {
            var searchListRequest = _youTubeVideoService.Search.List("snippet");
            searchListRequest.Q = searchText;
            searchListRequest.MaxResults = maxNoOfItems;
            searchListRequest.Type = "video";

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videos = searchListResponse.Items.Select(m => new YouTubeVideoModel
            {
                VideoTitle = m.Snippet.Title,
                VideoText = m.Snippet.Description,
                VideoId = m.Id.VideoId,
                VideoThumbnailUrl = m.Snippet.Thumbnails.Medium.Url
            }).ToList();

           SaveVideos(videos, pageId); 

            return videos;
        }

        public void SaveVideos(List<YouTubeVideoModel> videos, int pageId)
        {
            var content = _contentService.GetById(pageId);

            var nestedContent = new List<Dictionary<string, object>>();

            foreach (var item in videos)
            {
                var dic = new Dictionary<string, object>();
                dic.Add("ncContentTypeAlias", VideoItem.ModelTypeAlias);
                dic.Add("key", Guid.NewGuid());
                dic.Add("name", HttpUtility.HtmlDecode(item.VideoTitle));
                dic.Add("videoId", item.VideoId);
                dic.Add("videoTitle", HttpUtility.HtmlDecode(item.VideoTitle));
                dic.Add("videoText", item.VideoText);
                dic.Add("videoThumbnailUrl", item.VideoThumbnailUrl);

                nestedContent.Add(dic);
            }

            var jsonNestedContent = JsonConvert.SerializeObject(nestedContent);

            content.SetValue("youTubeVideos", jsonNestedContent);

            _contentService.SaveAndPublish(content);
        }
    }
}