using System;
using System.ComponentModel;

namespace MVVM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerModel playerModel = new PlayerModel("Hero", 100);
            PlayerViewModel playerViewModel = new PlayerViewModel(playerModel);

            playerViewModel.PropertyChanged += ViewModel_PropertyChanged;

            bool isRunning = true;
            while (isRunning)
            {
                var input = Console.ReadKey(intercept: true);

                if (input.Key == ConsoleKey.Spacebar)
                {
                    playerViewModel.TakeDamage(15);
                }
                else if (input.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                }
            }
        }
        private static void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is PlayerViewModel vm)
            {
                if (e.PropertyName == nameof(vm.Health))
                {
                    RenderView(vm);
                }
            }
        }
        private static void RenderView(PlayerViewModel vm)
        {
            Console.Clear();
            Console.WriteLine("--- MVVM Console RPG ---");
            Console.WriteLine($"Player: {vm.Name}");
            Console.WriteLine($"Health: {vm.Health} HP");
            Console.WriteLine("\nControls:");
            Console.WriteLine("[Space] Take 15 Damage");
            Console.WriteLine("[Esc] Exit");
        }
    }
}