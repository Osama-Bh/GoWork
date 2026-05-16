using System.Security.Claims;

namespace GoWork.Services.CurrentUserService
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? new ClaimsPrincipal();

        public int UserId
        {
            get
            {
                var value = User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(value, out var userId))
                    return -1;

                return userId;
            }
        }
    }
}
