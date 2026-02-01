using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoWork.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("test-auth")]
        public IActionResult TestAuth()
        {
            return Ok(new
            {
                Message = "Authentication successful",
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Roles = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList()
            });
        }
    }
}
