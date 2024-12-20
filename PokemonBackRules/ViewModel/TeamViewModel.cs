﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokemonBackRules.Interfaces;
using PokemonBackRules.Model;
using PokemonBackRules.Models;
using PokemonBackRules.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonBackRules.ViewModel
{
    public partial class TeamViewModel : ViewModelBase
    {

        private static readonly Random _random = new();
        public ObservableCollection<StackPanelItemModel> Items { get; set; }
        public ObservableCollection<List<string>> PokeTypes { get; } = new();

        [ObservableProperty]
        public string _NumPokemons;

        [ObservableProperty]
        public int _PokemonType;

        private readonly IHttpJsonClientProvider<PokemonApiModel> _httpJsonClientProvider;

        public TeamViewModel(IHttpJsonClientProvider<PokemonApiModel> httpJsonClientProvider)
        {
            _httpJsonClientProvider = httpJsonClientProvider;
            Items = new ObservableCollection<StackPanelItemModel>();
        }

        public override async Task LoadAsync()
        {
            Items.Clear();
            PokeTypes.Clear();

            List<PokemonApiModel> requestData = await _httpJsonClientProvider.GetAll(Constantes.POKE_TEAM_URL) ?? new List<PokemonApiModel>();

            var dictPokeLevel = requestData.Where(x => x.Catch)
                              .GroupBy(x => new {x.Id, x.Shiny})
                              .Select(x => new { Pokemon = x.First(), Level = x.Count() });
            foreach (var element in dictPokeLevel)
            {
                PokeTypes.Add(new List<string> {element.Pokemon.FrontDefault,  element.Pokemon.Name + " Lvl: " + element.Level});                                
            }

            GenerateStackPanelItems();
        }        

        private async Task GenerateStackPanelItems()
        {
            
            for (int i = 0; i < PokeTypes.Count; i++)
            {
                Items.Add(new StackPanelItemModel
                {
                    ImagePath = PokeTypes[i][0],
                    PokemonName = PokeTypes[i][1]                  
                });
            }
        }
    }
}
