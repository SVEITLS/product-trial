using Microsoft.AspNetCore.Mvc;
using Product_trial.Auth;
using Product_trial.BLL.Services;
using Product_trial.DAL.Models;

namespace Product_trial.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IAuthService _authService;

        public TokenController(ILogger<TokenController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        /// <summary>
        /// Handles the login request.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the login operation.</returns>
        /// <response code="200">Returns the authentication result if successful.</response>
        /// <response code="500">Returns an internal server error if the login fails.</response>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO Login)
        {
            try
            {
                return Ok(await _authService.Login(Login.Email, Login.Password));
            }
            catch (Exception ex)
            {
                //as a general manner it is not good to get a specific code or return error based on credentials issues, considered as vulnerability
                _logger.LogError(ex, "Failure to login into account");
                return StatusCode(500);
            }
        }
    }
}
