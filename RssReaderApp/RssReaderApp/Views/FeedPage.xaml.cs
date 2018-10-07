using RssReaderApp.Models;
using RssReaderApp.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RssReaderApp.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class FeedPage : Page
    {
        public FeedPageViewModel ViewModel { get; } = new FeedPageViewModel();

        public FeedPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var id = (string)e.Parameter;
            if (await ViewModel.InitializeAsync(id))
            {
                Bindings.Update();
            }
            else
            {
                Frame.Navigate(typeof(InitPage));
                while (Frame.CanGoBack)
                {
                    Frame.BackStack.RemoveAt(0);
                }
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(FeedItemPage), ((FeedItem)e.ClickedItem).Url.AbsoluteUri);
        }
    }
}
