namespace MvcMusicStore.Mediatr
{
    public interface IRequest : IRequest<Unit> { }
    public interface IRequest<out TResponse> { }
}