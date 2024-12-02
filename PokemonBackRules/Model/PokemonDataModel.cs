using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokemonBackRules.Model
{
    public class PokemonDataModel 
    {
        public string DateStart { get; set; }

        public string DateEnd { get; set; }

        public string Name { get; set; }

        public double DamageDoneTrainer { get; set; }

        public double DamageReceivedTrainer { get; set; }

        public double DamageDonePokemon { get; set; }

        public bool Catch { get; set; }
    }
}
