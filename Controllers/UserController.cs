using AppointmentAppBackend.Interfaces;
using AppointmentAppBackend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace LoginAPI.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IJWTManagerService _jWTManager;

		public UsersController(IJWTManagerService jWTManager)
		{
			this._jWTManager = jWTManager;
		}

        [AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users user)
		{
			
			var token = _jWTManager.Authenticate(user);

			if (token == null)
			{
				return Unauthorized();
			}

			HttpContext.Session.SetString("token", token.Token);

			return Ok(token);
		}
	}
}
