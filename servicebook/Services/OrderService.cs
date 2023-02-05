using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using transport.Models;
using transport.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace transport.Services
{
    public interface IOrderService
    {
        int Create(CreateOrderDto dto, int userId);
        bool Delete(int id, ClaimsPrincipal user);
        bool Edit(int id, EditOrderDto dto, ClaimsPrincipal user);
        OrderDto Get(int id);
        PageResult<OrderDto> GetAll(OrderQuery query);
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

            var order = _mapper.Map<Order>(dto);
            order.CreatedById = userId;
            _dbContext.Orders.Add(order);

            var pickupAdress = _mapper.Map<PickupAdress>(dto);
            _dbContext.PickupAdresses.Add(pickupAdress);

            var destAdress = _mapper.Map<DestinationAdress>(dto);
            _dbContext.DestinationAdresses.Add(destAdress);

            _dbContext.SaveChanges();

            return order.Id;
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
        public PageResult<OrderDto> GetAll(OrderQuery query)
        {
            var baseQuery = _dbContext
                .Orders
                .Include(o => o.PickupAdress)
                .Include(o => o.DestinationAdress)
                .Where(o => (query.from == null &&
                            query.to == null) || (o.PickupAdress.Country == query.from && o.DestinationAdress.Country == query.to))
                .Skip(query.pageSize * (query.pageNumber - 1));

            if (!string.IsNullOrEmpty(query.sortBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Order, object>>>
                {
                    {nameof(Order.PalletPlace),o => o.PalletPlace },
                    {nameof(Order.Weight),o => o.Weight },
                    {nameof(Order.Price),o => o.Price }
                };

                var selectedColumn = columnSelectors[query.sortBy];

                baseQuery = query.sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var orders = baseQuery
                .Skip(query.pageSize * (query.pageNumber - 1))
                .Take(query.pageSize)
                .ToList();

            var totalItemsCount = baseQuery.Count();

            var ordersDtos = _mapper.Map<List<OrderDto>>(orders);

            var result = new PageResult<OrderDto>(ordersDtos, totalItemsCount, query.pageSize, query.pageNumber);

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

