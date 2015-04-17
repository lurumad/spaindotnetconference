using AutoMapper;
using MvcMusicStore.Features.Home.ViewModels;
using MvcMusicStore.Models;

namespace MvcMusicStore.Configuration
{
    public static class Mappers
    {
        public static void Configure()
        {
            Mapper.CreateMap<Album, TopSellingAlbumViewModel>()
                .ForMember(viewModel => viewModel.Id, album => album.MapFrom(a => a.AlbumId))
                .ForMember(viewModel => viewModel.Title, album => album.MapFrom(a => a.Title))
                .ForMember(viewModel => viewModel.ArtUrl, album => album.MapFrom(a => a.AlbumArtUrl));
        }
    }
}