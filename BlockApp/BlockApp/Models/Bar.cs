using Windows.System;

namespace BlockApp.Models
{
    public class Bar : GameObject
    {
        private float Dx { get; } = 3f;
        public override void Update(GameContext context)
        {
            if (context.Key == VirtualKey.Left)
            {
                X -= Dx;
                if (X < 0) { X = 0; }
            }
            if (context.Key == VirtualKey.Right)
            {
                X += Dx;
                if (X + Width > context.WorldWidth)
                {
                    X = context.WorldWidth - Width;
                }
            }
        }
    }
}
