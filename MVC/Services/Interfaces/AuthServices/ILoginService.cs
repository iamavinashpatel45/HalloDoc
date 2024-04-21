using Microsoft.AspNetCore.Http;
using Services.ViewModels;

namespace Services.Interfaces.AuthServices
{
    public interface ILoginService
    {
        UserDataModel auth(Login model,List<int> userType);

        String isTokenValid(HttpContext httpContext, List<int> userType);

        Task<bool> resetPasswordLinkSend(string email,HttpContext httpContext);

        SetNewPassword validatePasswordLink(string token);

        Task<bool> changePassword(int aspNetUserId, String password);

    }
}
