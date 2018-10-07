namespace BlockApp.Models
{
    public class Block : GameObject
    {
        private bool isLive = true;
        public bool IsLive
        {
            get => isLive;
            set => SetProperty(ref isLive, value);
        }

        public override void Update(GameContext context)
        {
        }
    }
}
