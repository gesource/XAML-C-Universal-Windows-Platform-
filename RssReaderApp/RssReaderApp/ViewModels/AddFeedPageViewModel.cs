using RssReaderApp.Models;

namespace RssReaderApp.ViewModels
{
    public class AddFeedPageViewModel
    {
        private RssReaderAppContext Model { get; } = RssReaderAppContext.Instance;

        public string Title
        {
            get => Model.FeedManager.InputFeed.Title;
            set => Model.FeedManager.InputFeed.Title = value;
        }
        public string Url
        {
            get => Model.FeedManager.InputFeed.FeedUrl;
            set => Model.FeedManager.InputFeed.FeedUrl = value;
        }

        public AddFeedPageViewModel()
        {
            Model.FeedManager.ResetInputFeed();
        }
    }
}
