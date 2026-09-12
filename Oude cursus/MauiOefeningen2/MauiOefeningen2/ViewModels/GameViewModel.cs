using CommunityToolkit.Mvvm.Input;
using MauiOefeningen2.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiOefeningen2.ViewModels
{
    public partial class GameViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<Game> games;

        [ObservableProperty]
        Game game;

        private readonly IGameRepository _gameRepository;

        public GameViewModel(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            Games = new ObservableCollection<Game>(_gameRepository.GetGames());
            Game = new();
        }

        [RelayCommand]
        public void NieuweGame()
        {
            Game = new();
        }

        [RelayCommand]
        public void ToevoegenGames()
        {
            Games.Add(Game);
            Game = new();
        }

        [RelayCommand]
        public void VerwijderGames()
        {
            Games.Remove(Game);
        }
    }
}
