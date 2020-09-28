using Dare.Web.Services.YouTubeVideo;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Dare.Web.Composers
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IYouTubeVideoService, YouTubeVideoService>();
        }
    }
}