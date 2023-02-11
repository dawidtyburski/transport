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

namespace transport.Controllers
{
    [Route("orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }
        [AllowAnonymous]
        [HttpGet("index")]
        public IActionResult Search() 
        {
            List<string> countries = Enum.GetNames(typeof(Countries.Countries)).ToList();
            return View(countries);
        }
        [AllowAnonymous]
        [HttpPost("index")]
        public IActionResult Search(string from)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _orderService.Create(dto, userId);
            return Created($"/api/order/{id}", null);
        }
        [HttpPut("order/{id}")]
        public ActionResult Edit([FromBody] EditOrderDto dto, [FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _orderService.Edit(id, dto, User);
            if(!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete("order/{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _orderService.Delete(id, User);
            
            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
        [AllowAnonymous]
        [HttpGet("all")]        
        public ActionResult<IEnumerable<OrderDto>> GetAll(string from, string to)
        {

            var orders = _orderService.GetAll(from, to);
            return View(orders);
        }
        [HttpGet("order/{id}")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<OrderDto>> Get([FromRoute] int id)
        {
            var order = _orderService.Get(id);

            if(order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
