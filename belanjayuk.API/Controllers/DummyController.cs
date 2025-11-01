using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace belanjayuk.API.Controllers
{
    [Route("/api/dummy")]
    [ApiController]
    [Authorize]
    public class DummyController : Controller
    {
        [HttpGet]
        public IActionResult GetUserIdentity()
        {
            // Mendapatkan User ID yang tersimpan di Token (Claims)
            var userId = User.FindFirst("ID")?.Value;
            var userName = User.FindFirst("Username")?.Value;

            return Ok(new
            {
                Message = "Akses berhasil! JWT token valid.",
                UserId = userId,
                Username = userName
            });
        }
    }
}
