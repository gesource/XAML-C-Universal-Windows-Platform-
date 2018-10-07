using System.Collections.Generic;
using System.Linq;
using Windows.System;

namespace BlockApp.Models
{
    public class GameContext
    {
        public float WorldWidth { get; } = 400f;
        public float WorldHeight { get; } = 800f;

        // ユーザーの入力しているキー
        public VirtualKey Key { get; set; }

        public Ball Ball { get; } = new Ball
        {
            Width = 30f,
            Height = 30f,
            X = 130f,
            Y = 600f,
        };

        public Bar Bar { get; } = new Bar
        {
            Height = 30f,
            Width = 100f,
            Y = 700f,
            X = 150f,
        };

        public Block[] Blocks { get; }
        public int Score { get; set; }
        public GameState GameState { get; set; } = GameState.Init;

        public GameContext()
        {
            var blockWidth = 90f;
            var blockHeight = 30f;
            var blockNum = 4;
            var blockLeftMargin = 20f;

            Blocks = Enumerable.Range(0, 16).Select(x => new Block
            {
                Height = blockHeight,
                Width = blockWidth,
                X = x % blockNum * blockWidth + blockLeftMargin,
                Y = x / blockNum * blockHeight,
            }).ToArray();
        }

        public IEnumerable<GameObject> GetGameObjects()
        {
            yield return Ball;
            yield return Bar;
            foreach (var block in Blocks)
            {
                yield return block;
            }
        }
    }
}
