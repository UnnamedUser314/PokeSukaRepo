using PokemonBackRules.Model;
using PokemonBackRules.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBackRules.ViewModel
{
    public partial class HistoricViewModel : ViewModelBase
    {

        public ObservableCollection<PokemonDataModel> PokemonsData { get; set; } 

        public override async Task LoadAsync()
        {
            GenerateData();


        }

        public async void GenerateData()
        {
            PokemonsData = new ObservableCollection<PokemonDataModel>();
            List<PokemonApiModel> requestData = await HttpJsonClient<PokemonApiModel>.GetAll(Constantes.POKE_TEAM_URL) ?? new List<PokemonApiModel>();
            foreach (var item in requestData)
            {
                PokemonsData.Add(new PokemonDataModel { 
                    DateStart = item.DateStart, 
                    DateEnd =item.DateEnd , 
                    Name = item.Name, 
                    DamageDoneTrainer = item.DamageDoneTrainer,
                    DamageReceivedTrainer = item.DamageReceivedTrainer,
                    DamageDonePokemon = item.DamageDonePokemon, 
                    Catch = item.Catch});
            }
        }
    }
}
