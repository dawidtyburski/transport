using Microsoft.AspNetCore.Mvc;
using servicebook.Models;
using System.Diagnostics;
using transport;
using transport.Models;

namespace servicebook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly transportDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, transportDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            _dbContext.Add(new Order
            {
                
                Title = "Title",
                Description = "Description",
                Weight = 100,
                PalletPlace = 3,
                Price = 100,
                InitialAdressId = 2,
                InitialAdress = new InitialAdress { City = "Krakow", PostCode = "30-000", CountryId = 20, Country = _dbContext.Countries.First(o => o.Id == 20) },
                DestinationAdressId = 2,
                DestinationAdress = new DestinationAdress { City = "Warszawa", PostCode = "01-000", CountryId = 20, Country = _dbContext.Countries.First(o => o.Id == 20) },
                PrincipalId = 2,
                Principal = new Principal { Name = "Prima", ContactEmail = "p@p.pl", AccessCode = "aaa", PrincipalAdressId = 1, PrincipalAdress = _dbContext.PrincipalsAdresses.First(o => o.Id == 1) }
            });
            _dbContext.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}