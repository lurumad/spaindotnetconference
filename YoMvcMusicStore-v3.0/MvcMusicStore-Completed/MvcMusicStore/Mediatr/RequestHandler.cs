namespace MvcMusicStore.Mediatr
{
    public abstract class RequestHandler<TMessage> : IRequestHandler<TMessage, Unit>
        where TMessage : IRequest
    {
        public Unit Handle(TMessage message)
        {
            HandleCore(message);

            return Unit.Value;
        }

        protected abstract void HandleCore(TMessage message);
    }
}