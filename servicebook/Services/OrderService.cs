using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using transport.Models;
using transport.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace transport.Services
{
    public interface IOrderService
    {
        int Create(CreateOrderModel dto, ClaimsPrincipal user);
        bool Delete(int id, ClaimsPrincipal user);
        bool Edit(int id, EditOrderModel dto, ClaimsPrincipal user);
        OrderDto GetById(int id);
        IEnumerable<OrderDto> GetAll(string from, string to);
        IEnumerable<OrderDto> GetAllWithoutSearch();
    }
    public class OrderService : IOrderService
    {
        private readonly transportDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<CustomUser> _userManager;

        public OrderService(transportDbContext dbcontext, IMapper mapper, IAuthorizationService authorizationService,
                            UserManager<CustomUser> userManager)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public int Create(CreateOrderModel model, ClaimsPrincipal user)
        {

            /*var order = _mapper.Map<Order>(model);
            order.CustomUser = _userManager.GetUserAsync(user);

            _dbContext.Orders.Add(order);

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

             _dbContext.SaveChanges();

             return order.Id;*/
            return 1;
        }
        public bool Edit(int id, EditOrderModel dto, ClaimsPrincipal user)
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
                .Where(o => o.PickupAdress.Country == from && o.DestinationAdress.Country == to)
                .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);

            return result;
        }
        public IEnumerable<OrderDto> GetAllWithoutSearch()
        {
            var orders = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)               
                .ToList();

            var result = _mapper.Map<List<OrderDto>>(orders);

            return result;
        }
        public OrderDto GetById(int id)
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

