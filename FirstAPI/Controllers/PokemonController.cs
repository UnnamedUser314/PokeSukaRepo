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

        [HttpPost]
        public PokemonDTO Post([FromBody] PokemonDTO pokemon)
        {
            if (Pokemons.Any(x => x.Id == pokemon.Id))
            {
                return null;
            }
            Pokemons.Add(pokemon);
            return pokemon;
        }

        [HttpPut("{id}")]
        public PokemonDTO Put([FromBody] PokemonDTO pokemon, int id)
        {
            if (id != pokemon?.Id)
            {
                return null;
            }
            PokemonDTO? pokemonBBDD = Pokemons.FirstOrDefault(x => x.Id == pokemon.Id);
            if (pokemonBBDD == null)
            {
                return null;
            }
            pokemonBBDD.Name = pokemon.Name;
            pokemonBBDD.DamageDonePokemon = pokemon.DamageDonePokemon;
            pokemonBBDD.DamageDoneTrainer = pokemon.DamageDoneTrainer;
            pokemonBBDD.DamageReceivedTrainer = pokemon.DamageReceivedTrainer;
            pokemonBBDD.Catch = pokemon.Catch;
            pokemonBBDD.FrontDefault = pokemon.FrontDefault;   
            return pokemonBBDD;
        }

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
