using PokemonBackRules.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonBackRules.Services
{
    public class FileService<T> : IFileService<T> where T : class
    {
        public IEnumerable<T> Load(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return new List<T>();
                var content = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
           
        }

        public void Save(string filePath, IEnumerable<T> data)
        {
            var content = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, content);
        }
    }
}
