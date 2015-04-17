namespace MvcMusicStore.Mediatr
{
    public interface IAsyncRequest<out TResponse> { }

    public interface IAsyncRequest : IAsyncRequest<Unit> { }
}