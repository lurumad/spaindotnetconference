using System.Threading.Tasks;

namespace MvcMusicStore.Mediatr
{
    public interface IAsyncRequestHandler<in TRequest, TResponse>
        where TRequest : IAsyncRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest message);
    }
}