using RssReaderApp.ViewModels;
using Windows.UI.Xaml.Controls;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace RssReaderApp.Views
{
    public sealed partial class AddFeedPage : UserControl
    {
        public AddFeedPageViewModel ViewModel { get; } = new AddFeedPageViewModel();

        public AddFeedPage()
        {
            InitializeComponent();
        }
    }
}
