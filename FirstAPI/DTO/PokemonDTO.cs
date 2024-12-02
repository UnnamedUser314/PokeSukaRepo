using System.Text.Json.Serialization;

namespace FirstAPI.DTO
{
    public class PokemonDTO
    {


        public string FrontDefault { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public double DamageDoneTrainer { get; set; }
        public double DamageReceivedTrainer { get; set; }
        public double DamageDonePokemon { get; set; }
        public bool Catch { get; set; }

    }
}
