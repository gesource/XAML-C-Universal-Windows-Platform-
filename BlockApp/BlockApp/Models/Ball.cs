using System;
using System.Linq;
using Windows.Foundation;

namespace BlockApp.Models
{
    public class Ball : GameObject
    {
        private static float Speed { get; } = 5f;
        private float Dx { get; set; } = Speed;
        private float Dy { get; set; } = Speed;
        public Point Center
        {
            get => new Point(X + Width / 2, Y + Height / 2);
        }

        public override void Update(GameContext context)
        {
            // 移動
            X += Dx;
            Y += Dy;

            // エリアの境界とのあたり判定
            WorldHitTest(context);
            // バーとのあたり判定
            BarHitText(context);
            // ブロックとのあたり判定
            BlockHitTest(context);
        }

        private void WorldHitTest(GameContext context)
        {
            if (X + Width >= context.WorldWidth)
            {
                // 右端
                Dx = -Speed;
            }
            if (Y + Height >= context.WorldHeight)
            {
                // 下端
                context.Score--;
                Dy = -Speed;
            }
            if (X <= 0)
            {
                // 左端
                Dx = Speed;
            }
            if (Y <= 0)
            {
                // 上橋
                Dy = Speed;
            }
        }

        private void BarHitText(GameContext context)
        {
            var bar = context.Bar;
            // ボールから一番近いバーの座標を求める
            var x = Center.X;
            var y = Center.Y;
            if (bar.X > x)
            {
                x = bar.X;
            }
            else if (bar.X + bar.Width < x)
            {
                x = bar.X + bar.Width;
            }

            if (bar.Y > y)
            {
                y = bar.Y;
            }
            else if (bar.Y + bar.Height < y)
            {
                y = bar.Y + bar.Height;
            }

            // ボールとバーの一番近い座標と、ボールの中心点の距離を求める
            var distance = Math.Sqrt(Math.Pow(Math.Abs(x - Center.X), 2) + Math.Pow(Math.Abs(y - Center.Y), 2));
            // 当たっていたら移動方向を変える
            if (distance < Width / 2)
            {
                if (x == bar.X)
                {
                    // 左側面
                    Dx = -Speed;
                }
                else if (x == bar.X + bar.Width)
                {
                    // 右側面
                    Dx = Speed;
                }
                else
                {
                    Dy = -Dy;
                }
            }
        }

        private void BlockHitTest(GameContext context)
        {
            foreach (var block in context.Blocks.Where(x => x.IsLive))
            {
                // まだ生きているブロックにだけ処理をする
                var x = Center.X;
                var y = Center.Y;

                if (block.X > x)
                {
                    x = block.X;
                }
                else if (block.X + block.Width < x)
                {
                    x = block.X + block.Width;
                }

                if (block.Y > y)
                {
                    y = block.Y;
                }
                else if (block.Y + block.Height < y)
                {
                    y = block.Y + block.Height;
                }

                var distance = Math.Sqrt(Math.Pow(Math.Abs(x - Center.X), 2) + Math.Pow(Math.Abs(y - Center.Y), 2));
                if (distance < Width / 2)
                {
                    // ブロックとぶつかったのでスコアを1点増やす
                    context.Score++;
                    // 当たった
                    block.IsLive = false;
                    if (x == block.X)
                    {
                        // 左側面
                        Dx = -Speed;
                    }
                    else if (x == block.X + block.Width)
                    {
                        // 右側面
                        Dx = Speed;
                    }
                    else
                    {
                        Dy = -Dy;
                    }
                }
            }
        }
    }
}
