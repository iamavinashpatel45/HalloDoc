using Repositories.DataModels;

namespace Repositories.Interface
{
    public interface IAspRepository
    {
        int validateUser(String email,String password);

        int checkUser(String email);

        AspNetUser getUser(int aspNetUserId);

        Task<int> addUser(AspNetUser aspNetUser);

        Task<bool> setToken(String token, int aspNetUserId);

        bool checkToken(String token,int aspNetUserId);

        Task<bool> changePassword(AspNetUser aspNetUser);

        int checkUserRole(string role);

        Task<int> addUserRole(AspNetRole aspNetRole);

        Task<bool> addAspNetUserRole(AspNetUserRole aspNetUserRole);

        AspNetUserRole validateAspNetUserRole(string email, string password);
    }
}
