using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_trial.Auth;
using Product_trial.BLL.Services;
using Product_trial.DAL.Models;

namespace Product_trial.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }


        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="account">The account to create.</param>
        /// <returns></returns>
        /// <response code="200">Returns the newly created product.</response>
        /// <response code="500">If an error occurred while creating the product.</response>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            try
            {
                await _accountService.CreateAsync(account);   
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failure to create a new account");
                return StatusCode(500);
            }
        }
    }
}
