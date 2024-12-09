using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using PokemonBackRules.Model;
using PokemonBackRules.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonBackRules.Interfaces;
using PokemonBackRules.Services;
using CommunityToolkit.Mvvm.ComponentModel;


namespace PokemonBackRules.ViewModel
{
    public partial class HistoricViewModel : ViewModelBase
    {
        private readonly IFileService<PokemonDataModel> _fileService;
        private readonly IHttpJsonClientProvider<PokemonApiModel> _httpJsonClientProvider;

        
        //public ObservableCollection<PokemonDataModel> Pokemons { get; set; } 

        public HistoricViewModel (IFileService<PokemonDataModel> fileService, IHttpJsonClientProvider<PokemonApiModel> httpJsonClientProvider)
        {
            _fileService = fileService;
            _httpJsonClientProvider = httpJsonClientProvider;
            pokemonsData = new ObservableCollection<PokemonDataModel> ();
            
        }
        [ObservableProperty]

        private ObservableCollection<PokemonDataModel> pokemonsData;
        public override async Task LoadAsync()
        {
            GenerateData();


        }

        public async void GenerateData()
        {
            pokemonsData.Clear ();
            //List<PokemonApiModel> requestData = await HttpJsonClient<PokemonApiModel>.GetAll(Constantes.POKE_TEAM_URL) ?? new List<PokemonApiModel>();
            List<PokemonApiModel> requestData = await _httpJsonClientProvider.GetAll(Constantes.POKE_TEAM_URL) ?? new List<PokemonApiModel>();
            foreach (var item in requestData)
            {
                pokemonsData.Add(new PokemonDataModel { 
                    UniqueId = item.UniqueId,
                    FrontDefault = item.FrontDefault,
                    Name = item.Name,
                    Id = item.Id,
                    DateStart = item.DateStart, 
                    DateEnd =item.DateEnd ,                    
                    DamageDoneTrainer = item.DamageDoneTrainer,
                    DamageReceivedTrainer = item.DamageReceivedTrainer,
                    DamageDonePokemon = item.DamageDonePokemon, 
                    Catch = item.Catch,
                    Shiny = item.Shiny});
            }
        }

        [RelayCommand]
        public void SaveToFile()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = Constantes.JSON_FILTER
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _fileService.Save(saveFileDialog.FileName, pokemonsData);
            }
        }
    }
}
