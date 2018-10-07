using RssReaderApp.Commons;
using RssReaderApp.Models;
using System;
using System.Threading.Tasks;

namespace RssReaderApp.ViewModels
{
    public class FeedItemPageViewModel : BindableBase
    {
        private RssReaderAppContext Model { get; } = RssReaderAppContext.Instance;

        public Uri Uri { get => Model.FeedManager.CurrentFeed?.CurrentFeedItem?.Url ?? new Uri("about:blank"); }

        public async Task InitializeAsync(string id)
        {
            await Model.FeedManager.CurrentFeed.LoadAsync();
            Model.FeedManager.CurrentFeed.SetCurrentFeedItem(id);
        }
    }
}
