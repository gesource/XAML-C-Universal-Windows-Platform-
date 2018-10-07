namespace RssReaderApp.Models
{
    public class RssReaderAppContext
    {
        // 唯一のインスタンス
        public static RssReaderAppContext Instance { get; } = new RssReaderAppContext();

        public FeedManager FeedManager { get; } = new FeedManager();

        public void Store() => FeedManager.StoreFeeds();
        public void Restore() => FeedManager.RestoreFeeds();

        public void Activated()
        {
            Restore();
            FeedManager.RestoreCurrentFeed();
        }

        public void Suspending()
        {
            Store();
            FeedManager.StoreCurrentFeed();
        }
    }
}
