using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using transport.Models;
using transport.Services;
using transport.Countries;
using Microsoft.AspNetCore.Identity;

namespace transport.Controllers
{
    [Route("orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly transportDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<CustomUser> _userManager;

        public OrderController(transportDbContext dbcontext, IMapper mapper, IAuthorizationService authorizationService,
                            UserManager<CustomUser> userManager)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet("search")]
        public IActionResult Search() 
        {
            var model = new SearchOrderModel();
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost("search")]
        public IActionResult Search([FromForm]SearchOrderModel model)
        {
            model.countries = null;
            return RedirectToAction("GetAll", model);
        }
        [Authorize(Roles ="User")]
        [HttpGet("order/create")]
        public IActionResult Create()
        {
            var model = new CreateOrderModel();
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpPost("order/create")]
        public async Task<IActionResult> Create([FromForm]CreateOrderModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(model);
                var pickupAdress = new PickupAdress
                {
                    PostCode = model.PickupPostCode,
                    City = model.PickupCity,
                    Country = model.PickupCountry,
                    OrderId = order.Id
                };
                _dbContext.PickupAdresses.Add(pickupAdress);

                var destAdress = new DestinationAdress
                {
                    PostCode = model.DestPostCode,
                    City = model.DestCity,
                    Country = model.DestCountry,
                    OrderId = order.Id
                };
                _dbContext.DestinationAdresses.Add(destAdress);

                order.PickupAdress = pickupAdress;
                order.DestinationAdress = destAdress;
                var user = await _userManager.GetUserAsync(User);
                order.CustomUser = user;
                _dbContext.Orders.Add(order);
               
                _dbContext.SaveChanges();


                return Redirect($"{order.Id}");
            }
            return View(model);
            
        }
        [HttpPut("order/{id}")]
        public ActionResult Edit([FromBody] EditOrderDto dto, [FromRoute]int id)
        {
            /*if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _orderService.Edit(id, dto, User);
            if(!isUpdated)
            {
                return NotFound();
            }*/
            return Ok();
        }
        [HttpDelete("order/{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            /*var isDeleted = _orderService.Delete(id, User);
            
            if(isDeleted)
            {
                return NoContent();
            }
            */
            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet("all/search")]        
        public ActionResult<IEnumerable<OrderDto>> GetAll(SearchOrderModel model)
        {
            var orders = _dbContext
               .Orders
               .Include(o => o.PickupAdress)
               .Include(o => o.DestinationAdress)
               .Include(o => o.CustomUser)
               .Where(o => o.PickupAdress.Country == model.From && o.DestinationAdress.Country == model.To)
               .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);

            return View(result);
        }
        [AllowAnonymous]
        [HttpGet("all")]
        public ActionResult<IEnumerable<OrderDto>> GetAllWithoutSearch()
        {
            var orders = _dbContext
               .Orders
               .Include(o => o.PickupAdress)
               .Include(o => o.DestinationAdress)
               .Include(o => o.CustomUser)
               .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);

            return View(result);
        }
        [AllowAnonymous]
        [HttpGet("order/{id}")]        
        public ActionResult<OrderDto> GetById([FromRoute]int id)
        {
            var order = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)
                .Include(o=>o.CustomUser)
                .FirstOrDefault(o => o.Id == id);

            if (order is null) return View();

            var result = _mapper.Map<OrderDto>(order);


            if (result is null)
            {
                return View();
            }

            return View(result);
        }
    }
}
