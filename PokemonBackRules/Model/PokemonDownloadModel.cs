using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBackRules.Model
{
    public class PokemonDownloadModel
    {
        public string FrontDefault {  get; set; }
        public string Name { get; set; }
        public int Id { get; set; }

        public string DateStart { get; set; }

        public string DateEnd { get; set; }

        public string DamageDoneTrainer { get; set; }

        public string DamageReceivedTrainer { get; set; }

        public string DamageDonePokemon { get; set; }

        public bool Catch { get; set; }
    }
}
