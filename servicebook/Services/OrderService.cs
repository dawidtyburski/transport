using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using transport.Models;
using transport.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace transport.Services
{
    public interface IOrderService
    {
        int Create(CreateOrderDto dto, int userId);
        bool Delete(int id, ClaimsPrincipal user);
        bool Edit(int id, EditOrderDto dto, ClaimsPrincipal user);
        OrderDto Get(int id);
        IEnumerable<OrderDto> GetAll(string from, string to);
    }
    public class OrderService : IOrderService
    {
        private readonly transportDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public OrderService(transportDbContext dbcontext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public int Create(CreateOrderDto dto, int userId)
        {

            /* var order = _mapper.Map<Order>(dto);
             order.UserId = userId.ToString();
             _dbContext.Orders.Add(order);

             var pickupAdress = _mapper.Map<PickupAdress>(dto);
             _dbContext.PickupAdresses.Add(pickupAdress);

             var destAdress = _mapper.Map<DestinationAdress>(dto);
             _dbContext.DestinationAdresses.Add(destAdress);

             _dbContext.SaveChanges();

             return order.Id;*/
            return 1;
        }
        public bool Edit(int id, EditOrderDto dto, ClaimsPrincipal user)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);
            
            if (order is null) return false;

            var authorizationResult = _authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded) return false;
            order.Price = dto.Price;

            _dbContext.SaveChanges();

            return true;
        }
        public bool Delete(int id,ClaimsPrincipal user)
        {
            var order = _dbContext
                .Orders
                .FirstOrDefault(o => o.Id == id);

            if (order is null) return false;
            
            var authorizationResult = _authorizationService.AuthorizeAsync(user, order, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded) return false;
            
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
            return true;

        }
        public IEnumerable<OrderDto> GetAll(string from, string to)
        {
            var orders = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)
                .Where(o => (from==null&&
                            to==null) || (o.PickupAdress.Country == from && o.DestinationAdress.Country == to))
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

