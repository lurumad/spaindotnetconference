using System.Threading.Tasks;

namespace MvcMusicStore.Mediatr
{
    public abstract class AsyncRequestHandler<TMessage> : IAsyncRequestHandler<TMessage, Unit>
        where TMessage : IAsyncRequest
    {
        public async Task<Unit> Handle(TMessage message)
        {
            await HandleCore(message);

            return Unit.Value;
        }

        protected abstract Task HandleCore(TMessage message);
    }
}