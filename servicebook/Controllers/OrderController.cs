using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transport.Models;
using transport.Services;

namespace transport.Controllers
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService= orderService;
        }
        
        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _orderService.CreateOrder(dto);
            return Created($"/api/order/{id}", null);
        }
        [HttpPut("{id}")]
        public ActionResult Edit([FromBody] EditOrderDto dto, [FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _orderService.Edit(id, dto);
            if(!isUpdated)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute]int id)
        {
            var isDeleted = _orderService.Delete(id);
            
            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll()
        {
            var orders = _orderService.GetAll();

            return Ok(orders);
        }
        [HttpGet("{id}")]
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
