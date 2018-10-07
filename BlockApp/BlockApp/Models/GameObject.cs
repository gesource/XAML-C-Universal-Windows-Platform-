using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace BlockApp.Models
{
    public abstract class GameObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private float x;
        public float X
        {
            get => x;
            set
            {
                SetProperty(ref x, value);
                OnPropertyChanged(nameof(Margin));
            }
        }

        private float y;
        public float Y
        {
            get => y;
            set
            {
                SetProperty(ref y, value);
                OnPropertyChanged(nameof(Margin));
            }
        }

        private float width;
        public float Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }

        private float height;
        public float Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) { return false; }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public Thickness Margin
        {
            get => new Thickness(X, Y, 0, 0);
        }

        public abstract void Update(GameContext context);
    }
}
