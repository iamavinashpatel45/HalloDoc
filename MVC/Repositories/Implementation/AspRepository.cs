using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.DataModels;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class AspRepository : IAspRepository
    {
        private readonly HalloDocDbContext _dbContext;

        public AspRepository(HalloDocDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int validateUser(String email, String password)
        {
            AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email.Trim() == email.Trim() && a.PasswordHash==password);
            return aspNetUser?.Id ?? 0;
        }

        public int checkUser(String email)
        {
            AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Email.Trim() == email.Trim());
            return aspNetUser?.Id ?? 0;
        }

        public async Task<int> addUser(AspNetUser aspNetUser)
        {
            _dbContext.AspNetUsers.Add(aspNetUser);
            await _dbContext.SaveChangesAsync();
            return aspNetUser?.Id ?? 0;
        }

        public bool checkToken(String token, int aspNetUserId)
        {
            AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Id == aspNetUserId && a.ResetPasswordToken == token);
            return aspNetUser!=null?true:false;
        }

        public async Task<bool> setToken(String token, int aspNetUserId)
        {
            AspNetUser aspNetUser = _dbContext.AspNetUsers.FirstOrDefault(a => a.Id == aspNetUserId);
            if(aspNetUser!=null)
            {
                aspNetUser.ResetPasswordToken = token;
                _dbContext.AspNetUsers.Update(aspNetUser);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public AspNetUser getUser(int aspNetUserId)
        {
            return _dbContext.AspNetUsers.FirstOrDefault(a => a.Id == aspNetUserId);
        }

        public async Task<bool> changePassword(AspNetUser aspNetUser)
        {
            _dbContext.Update(aspNetUser); 
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public int checkUserRole(string role)
        {
            AspNetRole aspNetRole = _dbContext.AspNetRoles.FirstOrDefault(a => a.Name.Trim() == role);
            return aspNetRole?.Id ?? 0;
        }

        public async Task<int> addUserRole(AspNetRole aspNetRole)
        {
            _dbContext.AspNetRoles.Add(aspNetRole);
            await _dbContext.SaveChangesAsync();
            return aspNetRole?.Id ?? 0;
        }

        public async Task<bool> addAspNetUserRole(AspNetUserRole aspNetUserRole)
        {
            _dbContext.AspNetUserRoles.Add(aspNetUserRole);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public AspNetUserRole validateAspNetUserRole(String email, String password)
        {
            return _dbContext.AspNetUserRoles.Include(a => a.User).Include(a => a.Role).
                       FirstOrDefault(a => a.User.Email == email && a.User.PasswordHash == password);
        }
    }
}

