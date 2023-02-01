using Microsoft.AspNetCore.Identity;
using transport.Models;

namespace transport.Services
{
    public interface IAccountService
    {
        bool RegisterUser(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly transportDbContext _dbContext;

        public AccountService(transportDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RegisterUser(RegisterUserDto dto) 
        {
           
            if(_dbContext.Users.Any(u => u.Email == dto.Email))
            {
                return false;
            }
            var newUser = new User
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                RoleId = dto.RoleId
            };
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            newUser.PasswordHash = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
