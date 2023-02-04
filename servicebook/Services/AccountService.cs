using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using transport.Models;

namespace transport.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);
        bool RegisterUser(RegisterUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly transportDbContext _dbContext;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(transportDbContext dbContext, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users
                .Include(r => r.Role)
                .FirstOrDefault(u => u.Email== dto.Email);
            if(user is null)
            {
                return string.Empty;
            }

            bool result = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if(!result)
            {
                return string.Empty;
            }
            

            
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(7);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public bool RegisterUser(RegisterUserDto dto) 
        {           
            if(_dbContext.Users.FirstOrDefault(u => u.Email == dto.Email) is not null)
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
