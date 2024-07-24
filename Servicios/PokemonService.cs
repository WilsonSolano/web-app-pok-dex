using pokedex_mvc.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System;

namespace pokedex_mvc.Servicios
{
    public class PokemonService
    {
        public async Task<List<Pokemon>> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();

            using (var cliente = new HttpClient())
            {
                string url = "https://pokeapi.co/api/v2/pokemon/";
                cliente.DefaultRequestHeaders.Clear();

                var response = await cliente.GetAsync(url);

                if (response.IsSuccessStatusCode) 
                {
                    string jsonString = await response.Content.ReadAsStringAsync();
                    JObject jsonObj = JObject.Parse(jsonString);
                    var resultado = jsonObj["results"] as JArray;

                    foreach (var resultadoF in resultado)
                    {
                        string pokemonNombre = resultadoF["name"].ToString();
                        var pokemon = await obtenerDatosEspecificos(pokemonNombre);

                        if (pokemon != null)
                        {
                            lista.Add(pokemon);
                        }
                    }
                }
                return lista;
            }
        }

        public async Task<Pokemon> obtenerDatosEspecificos(string IDoNombrePokemon)
        {
            using (var cliente = new HttpClient())
            {
                string url = "https://pokeapi.co/api/v2/pokemon/";
                cliente.DefaultRequestHeaders.Clear();
                var respuesta = await cliente.GetAsync(String.Concat(url,IDoNombrePokemon));

                if (respuesta.IsSuccessStatusCode)
                {
                    var jsonString = await respuesta.Content.ReadAsStringAsync();
                    var jsonObj = JObject.Parse(jsonString);

                    return new Pokemon
                    {
                        Id = (int)jsonObj["id"],
                        Name = jsonObj["forms"]?[0]?["name"]?.ToString() ?? "N/A",
                        Tipos = jsonObj["types"] != null && jsonObj["types"] is JArray array
                            ? array.Select(t => t["type"]?["name"]?.ToString()).Where(name => !string.IsNullOrEmpty(name)).ToList() : new List<string?>(),
                        Url = jsonObj["sprites"]?["other"]?["dream_world"]?["front_default"]?.ToString() ?? "N/A",
                        Peso = (double?)jsonObj["weight"] ?? 0,
                        Altura = (double?)jsonObj["height"] ?? 0,
                        Movimientos = jsonObj["moves"] != null && jsonObj["moves"] is JArray movesArray
                            ? movesArray.Take(3).Select(movi => movi["move"]?["name"]?.ToString()).Where(name => !string.IsNullOrEmpty(name)).ToList() : new List<string?>(),
                        Hp = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "hp")?["base_stat"]) ?? 0,
                        Atk = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "attack")?["base_stat"]) ?? 0,
                        Def = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "defense")?["base_stat"]) ?? 0,
                        Satk = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "special-attack")?["base_stat"]) ?? 0,
                        Sdef = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "special-defense")?["base_stat"]) ?? 0,
                        Spd = (int?)(jsonObj["stats"]
                            ?.FirstOrDefault(stat => stat["stat"]?["name"]?.ToString() == "speed")?["base_stat"]) ?? 0
                    };
                }
                return null;

            }

        }
    }
}
