using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transport.Models;

namespace transport.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly transportDbContext _dbContext;

        public OrderController(transportDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll()
        {
            var orders = _dbContext
                .Orders
                .ToList();

            var ordersDtos = orders.Select(o => new OrderDto()
            {
                Title = o.Title,
                Description = o.Description,
                Weight = o.Weight,
                PalletPlace = o.PalletPlace,
                Price = o.Price,
                PostCode1 = o.InitialAdress.PostCode,
                City1 = o.InitialAdress.City,
                Country1 = o.InitialAdress.Country.CountryName,
                PostCode2 = o.DestinationAdress.PostCode,
                City2 = o.DestinationAdress.City,
                Country2 = o.DestinationAdress.Country.CountryName,
                Principal = o.Principal.Name

            });

            return Ok(ordersDtos);
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<OrderDto>> GetPrincipalOrders([FromRoute] int id)
        {
            var principalOrders = _dbContext
                .Orders
                .Include(o => o.InitialAdress)
                .Include(o => o.DestinationAdress)
                .Include(o => o.Principal)
                .ToList().Where(o => o.PrincipalId == id);
            
            
            if (principalOrders is null)
            {
                return NotFound();
            }

            var principalOrdersDtos = principalOrders.Select(o => new OrderDto()
            {
                Title = o.Title,
                Description = o.Description,
                Weight = o.Weight,
                PalletPlace = o.PalletPlace,
                Price = o.Price,
                PostCode1 = o.InitialAdress.PostCode,
                City1 = o.InitialAdress.City,
                Country1 = o.InitialAdress.Country.CountryName,
                PostCode2 = o.DestinationAdress.PostCode,
                City2 = o.DestinationAdress.City,
                Country2 = o.DestinationAdress.Country.CountryName,
                Principal = o.Principal.Name

            });

            return Ok(principalOrdersDtos);
        }
    }
}
