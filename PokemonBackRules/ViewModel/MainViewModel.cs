using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokemonBackRules.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;

        public MainViewModel(PokeSukaViewModel pokeSukaViewModel, TeamViewModel teamViewModel, HistoricViewModel historicViewModel)
        {
            
            PokeSukaViewModel = pokeSukaViewModel;
            HistoricViewModel = historicViewModel;
            TeamViewModel = teamViewModel;
            SelectedViewModel = pokeSukaViewModel;
        }
        public ViewModelBase? SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }

        public PokeSukaViewModel PokeSukaViewModel { get; set; }
        public HistoricViewModel HistoricViewModel { get; set; }
        public TeamViewModel TeamViewModel { get; set; }

        public async override Task LoadAsync()
        {
            if (SelectedViewModel is not null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }

        [RelayCommand]
        private async void SelectViewModel(object? parameter)
        {
            SelectedViewModel = parameter as ViewModelBase;
            await LoadAsync();
        }
    }
}
