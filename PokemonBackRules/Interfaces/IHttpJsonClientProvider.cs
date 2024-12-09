using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonBackRules.Interfaces
{
    public interface IHttpJsonClientProvider<T> 
    {
        public Task<T?> Get(string url);
        public Task<List<T?>> GetAll(string url);
        public Task Post(string url, object data);
        public Task Put(string url, object data);
        public Task DeleteAll(string url);
    }
}
