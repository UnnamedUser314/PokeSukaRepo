using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PokemonBackRules.Model;
using PokemonBackRules.Models;
using PokemonBackRules.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Configuration;
using PokemonBackRules.Interfaces;


namespace PokemonBackRules.ViewModel
{
    public partial class PokeSukaViewModel : ViewModelBase
    {
        private readonly IHttpJsonClientProvider<OnePokemonModel> _httpJsonClientProviderApi;
        private readonly IHttpJsonClientProvider<PokemonApiModel> _httpJsonClientProviderMiApi;

        private static readonly Random _random = new();
        private static double pokemonCurrentHealth = 0;
        private static int pokemonTotalHealth = 0;
        private static double myCurrentHealth = 1000;
        private static string? pokemonName = null;
        private static int pokemonId = 0;
        private static string? dateStart=null;
        private static string? dateEnd=null;
        private static double damageDoneByMe = 0;
        private static double damageDoneByPokemon = 0;
        private static bool shiny = false;
        private static Guid uniqueId;

        [ObservableProperty]
        public string ataque;

        [ObservableProperty]
        public string fronti;

        [ObservableProperty]
        public double pokeHpLeft;

        [ObservableProperty]
        public double myHpLeft;


        public PokeSukaViewModel(IHttpJsonClientProvider<OnePokemonModel> httpJsonClientProviderApi, IHttpJsonClientProvider<PokemonApiModel> httpJsonClientProviderMiApi)
        {
            _httpJsonClientProviderApi = httpJsonClientProviderApi;
            _httpJsonClientProviderMiApi = httpJsonClientProviderMiApi;
        }

        public override async Task LoadAsync()
        {
            if (Fronti == null)
            {
                SpawnPokemon();
                
            }

        }

        [RelayCommand]
        public void AttackPokemon()
        {
            double myAttack = _random.Next(0, 40);
            damageDoneByMe += myAttack;
            pokemonCurrentHealth -= myAttack;
            PokeHpLeft = Math.Round((pokemonCurrentHealth / pokemonTotalHealth) * 100);

            if (pokemonCurrentHealth > 0)
            {
                double damagePoke = int.Parse(Ataque);
                damageDoneByPokemon += damagePoke;
                myCurrentHealth -= damagePoke;
                MyHpLeft = Math.Round((myCurrentHealth / 1000) * 100);
            } else
            {
                PokemonApiModel pokemon = CreatePokemonObj();
                pokemon.DamageDoneTrainer = damageDoneByMe.ToString();
                pokemon.DamageReceivedTrainer = damageDoneByPokemon.ToString();
                pokemon.DamageDonePokemon = (damageDoneByPokemon).ToString();

                _httpJsonClientProviderMiApi.Put(Constantes.POKE_TEAM_URL+"/byGuid/"+uniqueId, pokemon);
                SpawnPokemon();               
            }
            
        }

        [RelayCommand]
        public async Task PostToApiAsync()
        {
            try
            {
                int randomNumber = _random.Next(0,100);
                var pokemon = CreatePokemonObj();                  

                if (randomNumber > PokeHpLeft)
                {

                    if (shiny)
                    {
                        myCurrentHealth = 1000;
                    } else
                    {
                        myCurrentHealth += 50;
                    }

                    pokemon.DamageDoneTrainer = damageDoneByMe.ToString();
                    pokemon.DamageReceivedTrainer = damageDoneByPokemon.ToString();
                    pokemon.DamageDonePokemon = (damageDoneByPokemon - 50).ToString();
                    pokemon.Catch = true;

                    _httpJsonClientProviderMiApi.Put(Constantes.POKE_TEAM_URL + "/byGuid/" + uniqueId, pokemon);
                    SpawnPokemon();
                }                
                           
               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting to API: {ex.Message}");
            }
        }

        [RelayCommand]
        public void Escapar()
        {
            var pokemon = CreatePokemonObj();
            pokemon.DamageDoneTrainer = damageDoneByMe.ToString();
            pokemon.DamageReceivedTrainer = damageDoneByPokemon.ToString();
            pokemon.DamageDonePokemon = (damageDoneByPokemon).ToString();
            //TODO service HTTPCLINT
            _httpJsonClientProviderMiApi.Put(Constantes.POKE_TEAM_URL + "/byGuid/" + uniqueId, pokemon);
            SpawnPokemon();
        }

        // TODO RETURN NEW PokemonApiModel
        public PokemonApiModel CreatePokemonObj()
        {
            return new PokemonApiModel
            {
                UniqueId = uniqueId,
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = "FIGHTING",
                DamageReceivedTrainer = "FIGHTING",
                DamageDonePokemon = "FIGHTING",
                Catch = false,
                Shiny = shiny
            };
        }             

        public async void SpawnPokemon()
        {
            int randomNumber = _random.Next(1, 100);
            int pokemonNum = _random.Next(1, 100);
            PokeHpLeft = 100;
            MyHpLeft = Math.Round((myCurrentHealth / 1000) * 100);
            OnePokemonModel requestData = await _httpJsonClientProviderApi.Get(Constantes.POKE_TYPE_URL + pokemonNum) ?? new OnePokemonModel();
            Ataque = requestData.Stats.ElementAt(1).BaseStat.ToString();

            if (randomNumber > 95)
            {
                Fronti = requestData.sprites.front_shiny;
                shiny = true;
            } else
            {
                Fronti = requestData.sprites.front_default;
                shiny = false;
            }
            
            damageDoneByMe = 0;
            damageDoneByPokemon = 0;

            pokemonCurrentHealth = requestData.Stats[0].BaseStat;
            pokemonTotalHealth = requestData.Stats[0].BaseStat;
            pokemonName = requestData.Name;
            pokemonId = requestData.Id;
            dateStart = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss");
            uniqueId = Guid.NewGuid();
            


            var pokemon = CreatePokemonObj();
            _httpJsonClientProviderMiApi.Post(Constantes.POKE_TEAM_URL, pokemon);
        }
        



    }

}
