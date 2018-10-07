using RssReaderApp.Commons;
using RssReaderApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RssReaderApp.ViewModels
{
    public class FeedPageViewModel : BindableBase
    {
        private RssReaderAppContext Model { get; } = RssReaderAppContext.Instance;
        public string Title { get => Model.FeedManager.CurrentFeed?.Title ?? ""; }

        public ReadOnlyObservableCollection<FeedItem> FeedItems { get; private set; }

        public async Task<bool> InitializeAsync(string id)
        {
            if (!Model.FeedManager.TrySetCurrentFeed(id))
            {
                return false;
            }
            await Model.FeedManager.CurrentFeed.LoadAsync();
            FeedItems = new ReadOnlyObservableCollection<FeedItem>(Model.FeedManager.CurrentFeed.FeedItems);
            return true;
        }
    }
}
