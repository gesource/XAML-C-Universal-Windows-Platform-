using BlockApp.Models;
using System;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace BlockApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Game Game { get; } = new Game();

        private DispatcherTimer Timer { get; }

        public MainPage()
        {
            InitializeComponent();
            Timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10)
            };
            Timer.Tick += Timer_Tick;
            Timer.Start();

            // ユーザーのキー入力をGameContextに設定する
            Window.Current.CoreWindow.KeyDown += (_, e) => Game.GameContext.Key = e.VirtualKey;
            Window.Current.CoreWindow.KeyUp += (_, e) => Game.GameContext.Key = VirtualKey.None;
        }

        private void Timer_Tick(object sender, object e)
        {
            Game.Update();
            Bindings.Update();
            switch(this.Game.GameContext.GameState)
            {
                case GameState.Init:
                    VisualStateManager.GoToState(this, "InitStae", true);
                    break;
                case GameState.Playing:
                    VisualStateManager.GoToState(this, "Playing", true);
                    break;
                case GameState.Goal:
                    VisualStateManager.GoToState(this, "GoalState", true);
                    break;
            }
        }
    }
}
