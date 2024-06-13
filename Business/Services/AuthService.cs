using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class AuthService
    {
        private readonly DefaultContext _context;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(DefaultContext context, JwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<int> Register(User user)
        {
            int result = 0;
            var passwordValid = await ValidatePassword(user.Password);
            if (passwordValid)
            {
                var entity = new Data.Entities.User
                {
                    Username = user.Username,
                    Password = user.Password
                };
                if (!await _context.Users.AnyAsync(u => u.Username == entity.Username))
                {
                    _context.Users.Add(entity);
                    await _context.SaveChangesAsync();
                    result = 1;
                }
            }
            
            return result;
        }

        public async Task<string> Login(User user)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (entity == null)
            {
                return null;
            }

            string token = _jwtTokenService.GenerateToken(entity.Id.ToString(), entity.Username);
            return token;
        }

        public async Task<bool> DeleteUser(User user)
        {
            var entity = await _context.Users.FindAsync(user.Id);

            if (entity == null)
            {
                return false;
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidatePassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            Match match = regex.Match(password);
            return match.Success;
        }
    }
}
