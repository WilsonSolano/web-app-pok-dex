using Microsoft.AspNetCore.Mvc;
using pokedex_mvc.Models;
using pokedex_mvc.Servicios;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace pokedex_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PokemonService _pokemonService;
        private readonly IMemoryCache _memoryCache;

        public HomeController(ILogger<HomeController> logger, PokemonService pokemonService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _pokemonService = pokemonService;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            List<Pokemon> listaPokemon;

            if (!_memoryCache.TryGetValue("ListaPokemon", out listaPokemon))
            {
                MemoryCacheEntryOptions cache = new MemoryCacheEntryOptions();
                cache.AbsoluteExpiration = DateTime.Now.AddMinutes(10);
                cache.Priority = CacheItemPriority.Normal;

                List<Pokemon> lista = await _pokemonService.listar();

                _memoryCache.Set("ListaPokemon", lista);

                return View(lista);
            }
            return View(listaPokemon);
        }

        public async Task<IActionResult> DatosPokemon(string IDoNombre)
        {
            var pokemon = await _pokemonService.obtenerDatosEspecificos(IDoNombre);
            return View(pokemon);
        }

        [HttpPost]
        public async Task<IActionResult> Buscar(string IDoNombre)
        {
            if (!String.IsNullOrWhiteSpace(IDoNombre))
            {
                var pokemon = await _pokemonService.obtenerDatosEspecificos(IDoNombre);
                return View(pokemon);
            }
            else 
            {
                return View(null);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
