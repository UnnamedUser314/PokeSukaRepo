using FirstAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : Controller
    {
        private readonly ILogger<PokemonDTO> _logger;

        private static List<PokemonDTO> Pokemons = new List<PokemonDTO>()
        {
            
        };

        public PokemonController(ILogger<PokemonDTO> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllElement")]
        public IEnumerable<PokemonDTO> Get()
        {
            return Pokemons;
        }

        [HttpGet("{id}")]
        public PokemonDTO GetOne(int id)
        {
            return Pokemons.FirstOrDefault(x => x.Id == id);
        }

        [HttpGet("byGuid/{uniqueId}")]
        public IActionResult GetByGuid(Guid uniqueId)
        {
            var pokemon = Pokemons.FirstOrDefault(p => p.UniqueId == uniqueId);
            if (pokemon == null)
            {
                return NotFound($"Pokemon with Unique ID {uniqueId} not found.");
            }
            return Ok(pokemon);
        }

        [HttpPut("byGuid/{uniqueId}")]
        public IActionResult PutByGuid(Guid uniqueId, [FromBody] PokemonDTO updatedPokemon)
        {
            var pokemon = Pokemons.FirstOrDefault(p => p.UniqueId == uniqueId);

            if (pokemon == null)
            {
                return NotFound($"Pokemon with Unique ID {uniqueId} not found.");
            }

            // Update properties
            pokemon.Name = updatedPokemon.Name ?? pokemon.Name;
            pokemon.FrontDefault = updatedPokemon.FrontDefault ?? pokemon.FrontDefault;
            pokemon.DateStart = updatedPokemon.DateStart ?? pokemon.DateStart;
            pokemon.DateEnd = updatedPokemon.DateEnd ?? pokemon.DateEnd;
            pokemon.DamageDoneTrainer = updatedPokemon.DamageDoneTrainer ?? pokemon.DamageDoneTrainer;
            pokemon.DamageReceivedTrainer = updatedPokemon.DamageReceivedTrainer ?? pokemon.DamageReceivedTrainer;
            pokemon.DamageDonePokemon = updatedPokemon.DamageDonePokemon ?? pokemon.DamageDonePokemon;
            pokemon.Catch = updatedPokemon.Catch;
            pokemon.Shiny = updatedPokemon.Shiny;

            return Ok(pokemon);
        }

        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll()
        {
            if (Pokemons.Count == 0)
            {
                return NoContent(); 
            }

            Pokemons.Clear();
            return Ok("All Pokémon data has been deleted.");
        }

        [HttpPost]
        public PokemonDTO Post([FromBody] PokemonDTO pokemon)
        {
            if (Pokemons.Any(x => x.UniqueId == pokemon.UniqueId))
            {
                return null;
            }
            Pokemons.Add(pokemon);
            return pokemon;
        }

        //[HttpPut("{id}")]
        //public PokemonDTO Put([FromBody] PokemonDTO pokemon, int id)
        //{
        //    if (id != pokemon?.Id)
        //    {
        //        return null;
        //    }
        //    PokemonDTO? pokemonBBDD = Pokemons.FirstOrDefault(x => x.Id == pokemon.Id);
        //    if (pokemonBBDD == null)
        //    {
        //        return null;
        //    }
        //    pokemonBBDD.Name = pokemon.Name;
            
        //    pokemonBBDD.DamageDonePokemon = pokemon.DamageDonePokemon;
        //    pokemonBBDD.DamageDoneTrainer = pokemon.DamageDoneTrainer;
        //    pokemonBBDD.DamageReceivedTrainer = pokemon.DamageReceivedTrainer;
        //    pokemonBBDD.Catch = pokemon.Catch;
        //    pokemonBBDD.FrontDefault = pokemon.FrontDefault;   
        //    return pokemonBBDD;
        //}

        [HttpDelete("{id}")]
        public bool Remove(int id)
        {
            PokemonDTO? pokemonBBDD = Pokemons.FirstOrDefault(x => x.Id == id);
            if (pokemonBBDD == null)
            {
                return false;
            }
            return Pokemons.Remove(pokemonBBDD);
        }
    }
}
