using System.Text.Json.Serialization;

namespace FirstAPI.DTO
{
    public class PokemonDTO
    {
        public Guid UniqueId { get; set; } 

        public string FrontDefault { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string DamageDoneTrainer { get; set; }
        public string DamageReceivedTrainer { get; set; }
        public string DamageDonePokemon { get; set; }
        public bool Catch { get; set; }
        public bool Shiny { get; set; }

    }
}
