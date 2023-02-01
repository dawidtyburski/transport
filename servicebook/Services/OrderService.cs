using AutoMapper;
using Microsoft.EntityFrameworkCore;
using transport.Models;

namespace transport.Services
{
    public interface IOrderService
    {
        int CreateOrder(CreateOrderDto dto);
        bool Delete(int id);
        bool Edit(int id, EditOrderDto dto);
        OrderDto Get(int id);
        IEnumerable<OrderDto> GetAll();
    }
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

            var pickupAdress = _mapper.Map<PickupAdress>(dto);
            _dbContext.PickupAdresses.Add(pickupAdress);

            var destAdress = _mapper.Map<DestinationAdress>(dto);
            _dbContext.DestinationAdresses.Add(destAdress);

            _dbContext.SaveChanges();

            return order.Id;
        }
        public bool Edit(int id, EditOrderDto dto)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);
            
            if (order is null) return false;

            order.Price = dto.Price;

            _dbContext.SaveChanges();

            return true;
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

