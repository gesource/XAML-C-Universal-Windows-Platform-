using Newtonsoft.Json;
using RssReaderApp.Commons;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace RssReaderApp.Models
{
    public class FeedManager : BindableBase
    {
        public ObservableCollection<Feed> Feeds { get; } = new ObservableCollection<Feed>()
        {
            // テスト用データ
            new Feed{ Title="かずきのBlog", FeedUrl="http://okazuki.hatenablog.com/feed" },
            new Feed{ Title="酢ろぐ！", FeedUrl="http://blog.ch3cooh.jp/feed" }
        };

        private Feed currentFeed;
        public Feed CurrentFeed { get => currentFeed; set => SetProperty(ref currentFeed, value); }

        private Feed inputFeed = new Feed();
        public Feed InputFeed { get => inputFeed; set => SetProperty(ref inputFeed, value); }

        private bool Restored { get; set; }

        public void ResetInputFeed()
        {
            InputFeed = new Feed();
        }

        public bool TryAddInputFeed()
        {
            if (InputFeed == null) { return false; }
            if (!InputFeed.ValidateProperty()) { return false; }
            Feeds.Add(InputFeed);
            InputFeed = new Feed();
            StoreFeeds();
            return true;
        }

        public void RemoveFeed(string id)
        {
            Feeds.Remove(Feeds.First(x => x.Id == id));
            if (CurrentFeed?.Id == id)
            {
                CurrentFeed = null;
            }
            StoreFeeds();
        }

        public Task LoadFeedsAsync()
        {
            return Task.WhenAll(Feeds.Select(x => x.LoadAsync()));
        }

        public bool TrySetCurrentFeed(string id)
        {
            CurrentFeed = Feeds.FirstOrDefault(x => x.Id == id);
            if (CurrentFeed == null)
            {
                // FeedItemから検索
                CurrentFeed = Feeds.FirstOrDefault(x => x.FeedItems.Any(y => y.Url.AbsoluteUri == id));
            }
            return CurrentFeed != null;
        }

        public void StoreFeeds()
        {
            var json = JsonConvert.SerializeObject(Feeds);
            ApplicationData.Current.RoamingSettings.Values[nameof(FeedManager)] = json;
        }

        public void RestoreFeeds()
        {
            if (Restored) { return; }
            Restored = true;
            if (!ApplicationData.Current.RoamingSettings.Values.TryGetValue(nameof(FeedManager), out object temp)) { return; }

            var restoredFeeds = JsonConvert.DeserializeObject<IEnumerable<Feed>>((string)temp);
            Feeds.Clear();
            foreach (var item in restoredFeeds)
            {
                Feeds.Add(item);
            }
        }

        public void StoreCurrentFeed()
        {
            ApplicationData.Current.LocalSettings.Values[nameof(FeedManager) + nameof(CurrentFeed)] = CurrentFeed?.Id;
        }

        public void RestoreCurrentFeed()
        {
            RestoreFeeds();
            if (!ApplicationData.Current.LocalSettings.Values.TryGetValue(nameof(FeedManager) + nameof(CurrentFeed), out object temp))
            {
                return;
            }
            if (temp == null) { return; }
            TrySetCurrentFeed((string)temp);
        }
    }
}
