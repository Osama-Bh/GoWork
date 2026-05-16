using System.Security.Claims;

namespace GoWork.Services.CurrentUserService
{
    public interface ICurrentUserService
    {
        int UserId { get; }

        ClaimsPrincipal User { get; }
    }
}
