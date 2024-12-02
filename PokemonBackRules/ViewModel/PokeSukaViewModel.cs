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


namespace PokemonBackRules.ViewModel
{
    public partial class PokeSukaViewModel : ViewModelBase
    {

        private static readonly Random _random = new();
        private static double pokemonCurrentHealth = 0;
        private static int pokemonTotalHealth = 0;
        private static double myCurrentHealth = 1000;
        private static string pokemonName = null;
        private static int pokemonId = 0;
        private static string dateStart=null;
        private static string dateEnd=null;
        private static double damageDoneByMe = 0;
        private static double damageDoneByPokemon = 0;

        [ObservableProperty]
        public string ataque;

        [ObservableProperty]
        public string fronti;

        [ObservableProperty]
        public double pokeHpLeft;

        [ObservableProperty]
        public double myHpLeft;


        public PokeSukaViewModel()
        {
            
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
            PokeHpLeft = (pokemonCurrentHealth / pokemonTotalHealth) * 100;

            if (pokemonCurrentHealth > 0)
            {
                double damagePoke = int.Parse(Ataque);
                damageDoneByPokemon += damagePoke;
                myCurrentHealth -= damagePoke;
                MyHpLeft = (myCurrentHealth/1000)*100;
            } else
            {
                var pokemon = CreatePokemonObj();
                HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
                SpawnPokemon();               
            }
            if(myCurrentHealth<=0)
            {
                var pokemon = CreatePokemonObj();
                HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
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
                    myCurrentHealth += 50;
                    //if (requestData.Any(x => x.Id == pokemonId))
                    //{                       
                    //    var poke = requestData.First(x => x.Id == pokemonId);
                    //    var newPokemon = CreateCapturedLevelincPokemonObj(poke.Level + 1);
                    //    HttpJsonClient<PokemonApiModel>.Put(Constantes.POKE_TEAM_URL+pokemonId, newPokemon);
                    //} else
                    //{
                    //    pokemon = CreateCapturedPokemonObj();
                    //    HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
                    //}
                    pokemon = CreateCapturedPokemonObj();
                    HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
                    SpawnPokemon();
                } else
                {
                                  
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
            SpawnPokemon();
        }
        
        public object CreatePokemonObj()
        {
            return new
            {
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                Level = 1,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = damageDoneByMe,
                DamageReceivedTrainer = damageDoneByPokemon,
                DamageDonePokemon = damageDoneByPokemon - 50,
                Catch = false
            };
        }

        public object CreateCapturedLevelincPokemonObj(int level)
        {
            return new
            {
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                Level = level,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = damageDoneByMe,
                DamageReceivedTrainer = damageDoneByPokemon,
                DamageDonePokemon = damageDoneByPokemon - 50,
                Catch = true
            };
        }

        public object CreateCapturedPokemonObj()
        {
            return new
            {
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                Level = 1,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = damageDoneByMe,
                DamageReceivedTrainer = damageDoneByPokemon,
                DamageDonePokemon = damageDoneByPokemon - 50,
                Catch = true
            };
        }


        public async void SpawnPokemon()
        {
            int pokemonNum = _random.Next(1, 100);
            PokeHpLeft = 100;
            MyHpLeft = (myCurrentHealth / 1000) * 100;
            OnePokemonModel requestData = await HttpJsonClient<OnePokemonModel>.Get(Constantes.POKE_TYPE_URL + pokemonNum) ?? new OnePokemonModel();
            Ataque = requestData.Stats.ElementAt(1).BaseStat.ToString();
            Fronti = requestData.sprites.front_default;
            pokemonCurrentHealth = requestData.Stats[0].BaseStat;
            pokemonTotalHealth = requestData.Stats[0].BaseStat;
            pokemonName = requestData.Name;
            pokemonId = requestData.Id;
            dateStart = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss");


        }
        



    }

}
