using System.Collections.Generic;
using MvcMusicStore.Features.Home.ViewModels;
using MvcMusicStore.Mediatr;

namespace MvcMusicStore.Features.Home.Requests
{
    public class TopSellingAlbumsRequest : IAsyncRequest<IEnumerable<TopSellingAlbumViewModel>>
    {
        public int MaximunResults { get; private set; }

        public TopSellingAlbumsRequest(int maximunResults)
        {
            MaximunResults = maximunResults;
        }
    }
}