using RssReaderApp.Commons;
using RssReaderApp.Models;
using System.Collections.ObjectModel;

namespace RssReaderApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private RssReaderAppContext Model { get; } = RssReaderAppContext.Instance;
        public ReadOnlyObservableCollection<Feed> Feeds { get; }

        public MainPageViewModel()
        {
            Feeds = new ReadOnlyObservableCollection<Feed>(Model.FeedManager.Feeds);
        }

        public bool TryAddFeed()
        {
            return Model.FeedManager.TryAddInputFeed();
        }

        public void RemoveFeed(string id)
        {
            Model.FeedManager.RemoveFeed(id);
        }

        public void Initialize()
        {
            Model.Restore();
        }
    }
}
