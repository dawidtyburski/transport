using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transport.Models;
using Microsoft.AspNetCore.Identity;

namespace transport.Controllers
{
    [Route("orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly transportDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<CustomUser> _userManager;

        public OrderController(transportDbContext dbcontext, IMapper mapper, UserManager<CustomUser> userManager)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
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
                user.Counter++;
                _dbContext.SaveChanges(); 
                TempData["CreateSuccess"] = "Order created successfully";              
            }
            return View(model);
            
        }
        
        [Authorize(Roles = "User")]
        [HttpGet("order/edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        [Authorize(Roles ="User")]
        [HttpPost("order/edit/{id}")]
        public ActionResult Edit([FromForm]EditOrderModel model, [FromRoute]int id)
        {
            if(ModelState.IsValid)
            {
                var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);

                order.Price = model.Price;

                _dbContext.SaveChanges();

                TempData["EditSuccess"] = "Price updated successfuly";
            }
            return View();
        }
        
        [Authorize(Roles ="User")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            return RedirectToAction("getuserorders", new { guid = Guid.NewGuid() });
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
       
        [Authorize(Roles = "User")]
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetUserOrders(Guid guid)
        {
            if(guid== Guid.Empty)
            {
                return RedirectToAction("GetAllWithoutSearch", "Order");
            }
            var orders = _dbContext
               .Orders
               .Include(o => o.PickupAdress)
               .Include(o => o.DestinationAdress)
               .Include(o => o.CustomUser)
               .Where(o => o.CustomUser.UserName == User.Identity.Name)
               .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);
            return View(result);
        }
    }
}
