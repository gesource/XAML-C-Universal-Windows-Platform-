using Newtonsoft.Json;
using RssReaderApp.Commons;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace RssReaderApp.Models
{
    public class Feed : BindableBase
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        private string title;
        public string Title { get => title; set => SetProperty(ref title, value); }

        private string feedUrl;
        public string FeedUrl { get => feedUrl; set => SetProperty(ref feedUrl, value); }

        private bool isLoaded;
        [JsonIgnore]
        public bool IsLoaded { get => isLoaded; set => SetProperty(ref isLoaded, value); }

        [JsonIgnore]
        public ObservableCollection<FeedItem> FeedItems { get; } = new ObservableCollection<FeedItem>();

        private FeedItem currentFeedItem;
        [JsonIgnore]
        public FeedItem CurrentFeedItem { get => currentFeedItem; set => SetProperty(ref currentFeedItem, value); }

        public void SetCurrentFeedItem(string url)
        {
            CurrentFeedItem = FeedItems.FirstOrDefault(x => x.Url.AbsoluteUri == url);
        }

        public async Task LoadAsync()
        {
            if (IsLoaded) { return; }
            IsLoaded = true;
            var reader = new SyndicationClient();
            try
            {
                var results = await reader.RetrieveFeedAsync(new Uri(FeedUrl));
                var feedItemSource = results.Items.Select(x => new FeedItem
                {
                    Title = x.Title.Text,
                    Url = x.Links.First().Uri,
                });
                FeedItems.Clear();
                foreach (var item in feedItemSource)
                {
                    FeedItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                FeedItems.Clear();
                FeedItems.Add(new FeedItem
                {
                    Title = ex.ToString(),
                    Url = new Uri("about:blank"),
                });
            }
        }

        /// <summary>
        /// 入力用のプロパティが正しいかどうかを返す
        /// </summary>
        /// <returns></returns>
        public bool ValidateProperty()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return false;
            }

            if (!Uri.TryCreate(FeedUrl, UriKind.Absolute, out Uri dummy))
            {
                return false;
            }

            return true;
        }
    }
}
