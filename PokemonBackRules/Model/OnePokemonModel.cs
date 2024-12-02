using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonBackRules.Model
{
    public class OnePokemonModel
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites sprites { get; set; }


        [JsonPropertyName("stats")]
        public List<PokemonStat> Stats { get; set; }
    }
    

}
