using System.Linq;

namespace BlockApp.Models
{
    public class Game
    {
        public GameContext GameContext { get; } = new GameContext();

        public void Update()
        {
            if (GameContext.GameState == GameState.Init)
            {
                if (GameContext.Key == Windows.System.VirtualKey.Space)
                {
                    GameContext.GameState = GameState.Playing;
                }
                return;
            }

            if (GameContext.GameState == GameState.Goal)
            {
                return;
            }

            foreach (var obj in GameContext.GetGameObjects())
            {
                obj.Update(GameContext);
            }

            if (!GameContext.Blocks.Where(x => x.IsLive).Any())
            {
                GameContext.GameState = GameState.Goal;
            }
        }
    }
}
