using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Dapper;
using MvcMusicStore.Features.Home.Requests;
using MvcMusicStore.Features.Home.ViewModels;
using MvcMusicStore.Mediatr;
using MvcMusicStore.Models;

namespace MvcMusicStore.Features.Home.Handlers
{
    public class TopSellingAlbumsHandler :
        IAsyncRequestHandler<TopSellingAlbumsRequest, IEnumerable<TopSellingAlbumViewModel>>
    {
        private readonly MusicStoreDbContext _dbContext;

        public TopSellingAlbumsHandler(MusicStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TopSellingAlbumViewModel>> Handle(TopSellingAlbumsRequest request)
        {
//            return await _dbContext.Database.Connection
//                .QueryAsync<TopSellingAlbumViewModel>(@"SELECT TOP (5) 
//    [Project1].[AlbumId] AS [Id], 
//    [Project1].[Title] AS [Title], 
//    [Project1].[AlbumArtUrl] AS [ArtUrl]
//    FROM ( SELECT 
//        [Extent1].[AlbumId] AS [AlbumId], 
//        [Extent1].[Title] AS [Title], 
//        [Extent1].[AlbumArtUrl] AS [AlbumArtUrl], 
//        (SELECT 
//            COUNT(1) AS [A1]
//            FROM [dbo].[OrderDetails] AS [Extent2]
//            WHERE [Extent1].[AlbumId] = [Extent2].[AlbumId]) AS [C1]
//        FROM [dbo].[Albums] AS [Extent1]
//    )  AS [Project1]
//    ORDER BY [Project1].[C1] DESC");

            return await _dbContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(request.MaximunResults)
                .Project()
                .To<TopSellingAlbumViewModel>()
                .ToListAsync();
        }
    }
}