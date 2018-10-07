using RssReaderApp.Commons;
using System;

namespace RssReaderApp.Models
{
    public class FeedItem : BindableBase
    {
        private string title;
        public string Title { get => title; set => SetProperty(ref title, value); }

        private Uri url;
        public Uri Url { get => url; set => SetProperty(ref url, value); }
    }
}
