﻿using CommunityToolkit.Mvvm.ComponentModel;
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
                var pokemon = CreateUncapturedPokemonObj();               
                HttpJsonClient<PokemonApiModel>.Put(Constantes.POKE_TEAM_URL+"/byGuid/"+uniqueId, pokemon);
                SpawnPokemon();               
            }
            //if(myCurrentHealth<=0)
            //{
            //    var pokemon = CreateCapturedPokemonObj();
            //    HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
            //    SpawnPokemon();                
            //}
            
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

                    pokemon = CreateCapturedPokemonObj();

                    HttpJsonClient<PokemonApiModel>.Put(Constantes.POKE_TEAM_URL + "/byGuid/" + uniqueId, pokemon);
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
            var pokemon = CreateUncapturedPokemonObj();
            HttpJsonClient<PokemonApiModel>.Put(Constantes.POKE_TEAM_URL + "/byGuid/" + uniqueId, pokemon);
            SpawnPokemon();
        }
        
        public object CreatePokemonObj()
        {
            return new
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
       


        public object CreateCapturedPokemonObj()
        {
            return new
            {
                UniqueId = uniqueId,
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = damageDoneByMe.ToString(),
                DamageReceivedTrainer = damageDoneByPokemon.ToString(),
                DamageDonePokemon = (damageDoneByPokemon - 50).ToString(),
                Catch = true,
                Shiny = shiny
            };
        }

        public object CreateUncapturedPokemonObj()
        {
            return new
            {
                UniqueId = uniqueId,
                FrontDefault = Fronti,
                Name = pokemonName,
                Id = pokemonId,
                DateStart = dateStart,
                DateEnd = DateTime.Now.ToString("yyyy/mm/ddThh:mm:ss"),
                DamageDoneTrainer = damageDoneByMe.ToString(),
                DamageReceivedTrainer = damageDoneByPokemon.ToString(),
                DamageDonePokemon = (damageDoneByPokemon).ToString(),
                Catch = false,
                Shiny = shiny
            };
        }


        public async void SpawnPokemon()
        {
            int randomNumber = _random.Next(1, 100);
            int pokemonNum = _random.Next(1, 100);
            PokeHpLeft = 100;
            MyHpLeft = (myCurrentHealth / 1000) * 100;
            OnePokemonModel requestData = await HttpJsonClient<OnePokemonModel>.Get(Constantes.POKE_TYPE_URL + pokemonNum) ?? new OnePokemonModel();
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
            HttpJsonClient<PokemonApiModel>.Post(Constantes.POKE_TEAM_URL, pokemon);
        }
        



    }

}
