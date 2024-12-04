using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonBackRules.Model
{
    class PokemonApiModel
    {
        [JsonPropertyName("frontDefault")]
        public string FrontDefault { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("dateStart")]
        public string DateStart { get; set; }
        [JsonPropertyName("dateEnd")]
        public string DateEnd { get; set; }
        [JsonPropertyName("damageDoneTrainer")]
        public string DamageDoneTrainer { get; set; }
        [JsonPropertyName("damageReceivedTrainer")]
        public string DamageReceivedTrainer { get; set; }
        [JsonPropertyName("damageDonePokemon")]
        public string DamageDonePokemon { get; set; }
        [JsonPropertyName("catch")]
        public bool Catch { get; set; }
    }

    public class Pokemon
    {
        
    }
}
