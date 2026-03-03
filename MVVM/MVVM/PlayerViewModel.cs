using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM
{
    internal class PlayerViewModel : INotifyPropertyChanged
    {
        private PlayerModel _player;

        public event PropertyChangedEventHandler? PropertyChanged;

        public PlayerViewModel(PlayerModel playerModel)
        {
            _player = playerModel;
        }

        public string Name
        {
            get => _player.Name;
        }
        public int Health
        {
            get => _player.Health;
            set
            {
                if (_player.Health != value)
                {
                    _player.Health = value;
                    OnPropertyChanged();
                }
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
