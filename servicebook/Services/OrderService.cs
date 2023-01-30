using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using transport.Models;

namespace transport.Services
{
    public class OrderService : IOrderService
    {
        private readonly transportDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(transportDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public int CreateOrder(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }
        public bool Delete(int id)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);

            if (order is null) return false;

            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            return true;

        }
        public IEnumerable<OrderDto> GetAll()
        {
            var orders = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)
                .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);

            return result;
        }
        public OrderDto Get(int id)
        {
            var order = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)
                .FirstOrDefault(o => o.Id == id);

            if (order is null) return null;

            var result = _mapper.Map<OrderDto>(order);

            return result;
        }
    }
}

