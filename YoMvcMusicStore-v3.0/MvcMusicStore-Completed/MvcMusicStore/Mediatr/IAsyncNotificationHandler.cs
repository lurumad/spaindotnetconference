using System.Threading.Tasks;

namespace MvcMusicStore.Mediatr
{
    public interface IAsyncNotificationHandler<in TNotification>
        where TNotification : IAsyncNotification
    {
        Task Handle(TNotification notification);
    }
}