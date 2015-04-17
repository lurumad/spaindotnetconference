namespace MvcMusicStore.Mediatr
{
    public interface INotificationHandler<in TNotification>
        where TNotification : INotification
    {
        void Handle(TNotification notification);
    }
}