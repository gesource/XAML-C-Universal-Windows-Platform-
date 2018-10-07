using RssReaderApp.Models;
using RssReaderApp.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace RssReaderApp
{
    /// <summary>
    /// 既定の Application クラスを補完するアプリケーション固有の動作を提供します。
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// 単一アプリケーション オブジェクトを初期化します。これは、実行される作成したコードの
        ///最初の行であるため、main() または WinMain() と論理的に等価です。
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        /// <summary>
        /// アプリケーションがエンド ユーザーによって正常に起動されたときに呼び出されます。他のエントリ ポイントは、
        /// アプリケーションが特定のファイルを開くために起動されたときなどに使用されます。
        /// </summary>
        /// <param name="e">起動の要求とプロセスの詳細を表示します。</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var mainPage = Window.Current.Content as MainPage;

            // ウィンドウに既にコンテンツが表示されている場合は、アプリケーションの初期化を繰り返さずに、
            // ウィンドウがアクティブであることだけを確認してください
            if (mainPage == null)
            {
                // ナビゲーション コンテキストとして動作するフレームを作成し、最初のページに移動します
                mainPage = new MainPage();
                mainPage.RootFrame.NavigationFailed += OnNavigationFailed;

                // 戻るボタン
                mainPage.RootFrame.Navigated += (_, __) =>
                {
                    var rootFrame = mainPage.RootFrame;
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                    rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
                };
                SystemNavigationManager.GetForCurrentView().BackRequested += (_, args) =>
                {
                    if (mainPage.RootFrame.CanGoBack)
                    {
                        mainPage.RootFrame.GoBack();
                        args.Handled = true;
                    }
                };

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // 以前中断したアプリケーションから状態を読み込みます
                    RssReaderAppContext.Instance.Activated();
                    mainPage.RootFrame.SetNavigationState((string)ApplicationData.Current.LocalSettings.Values["NavigationState"]);
                }

                // フレームを現在のウィンドウに配置します
                Window.Current.Content = mainPage;
            }

            if (mainPage.RootFrame.Content == null)
            {
                // 初期画面に遷移する
                mainPage.RootFrame.Navigate(typeof(InitPage));
            }

            // 現在のウィンドウがアクティブであることを確認します
            Window.Current.Activate();
        }

        /// <summary>
        /// 特定のページへの移動が失敗したときに呼び出されます
        /// </summary>
        /// <param name="sender">移動に失敗したフレーム</param>
        /// <param name="e">ナビゲーション エラーの詳細</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// アプリケーションの実行が中断されたときに呼び出されます。
        /// アプリケーションが終了されるか、メモリの内容がそのままで再開されるかに
        /// かかわらず、アプリケーションの状態が保存されます。
        /// </summary>
        /// <param name="sender">中断要求の送信元。</param>
        /// <param name="e">中断要求の詳細。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            // アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
            RssReaderAppContext.Instance.Suspending();
            ApplicationData.Current.LocalSettings.Values["NavigationState"] = ((MainPage)Window.Current.Content).RootFrame.GetNavigationState();
            deferral.Complete();
        }
    }
}
