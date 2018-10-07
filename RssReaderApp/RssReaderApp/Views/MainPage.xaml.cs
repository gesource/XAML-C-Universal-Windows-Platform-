using RssReaderApp.Models;
using RssReaderApp.ViewModels;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RssReaderApp.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            RootFrame.Navigate(typeof(FeedPage), ((Feed)e.ClickedItem).Id);
        }

        private async void AppBarButtonAddFeed_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var dlg = new ContentDialog
            {
                Title = "Feedの追加",
                Content = new AddFeedPage(),
                IsPrimaryButtonEnabled = true,
                IsSecondaryButtonEnabled = true,
                PrimaryButtonText = "追加",
                SecondaryButtonText = "キャンセル"
            };
            await ShowAsync(dlg);
        }

        private async Task ShowAsync(ContentDialog dlg)
        {
            var result = await dlg.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (!ViewModel.TryAddFeed())
                {
                    var errorMessage = new MessageDialog("タイトルとURLを入力してください。URLは正しい形式で入力してください。");
                    await errorMessage.ShowAsync();
                    await ShowAsync(dlg);
                }
            }
        }

        private void MenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var feed = (Feed)((FrameworkElement)sender).DataContext;
            ViewModel.RemoveFeed(feed.Id);
        }

        private void Grid_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.Initialize();
        }
    }
}
