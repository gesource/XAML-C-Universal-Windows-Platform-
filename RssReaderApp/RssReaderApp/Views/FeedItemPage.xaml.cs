using RssReaderApp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace RssReaderApp.Views
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class FeedItemPage : Page
    {
        public FeedItemPageViewModel ViewModel { get; } = new FeedItemPageViewModel();
        public FeedItemPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.InitializeAsync((string)e.Parameter);
            // 非同期になったためViewModelのバインドデータの読み込みより前に評価されるため
            // データの読み込みが終わった後に再実行する
            Bindings.Update();
        }
    }
}
