using Product_trial.BLL.Services;
using Product_trial.DAL.Models;
using Product_trial.DAL.Utils;

namespace Product_trial.Auth
{
    public interface IAuthService
    {
        public Task<string> Login(string email, string password);
    }
    public class AuthService(ILogger<TokenService> logger, IAccountService accountService, ITokenService tokenService) : IAuthService
    {
        public async Task<string> Login(string email, string password)
        {
            logger.LogDebug("Login being processed for User : {User}", email);
            Account? account = await accountService.GetAsync(email);

            if (account is null)
            {
                logger.LogError("The account with {Email} email does not exists", email);
                throw new AccountNotFoundException(email);
            }
            
            if(!HashHelper.Verify(password, account.Password))
            {
                logger.LogError("Incorrect password for account : {Email}",email);
                throw new AccountCredentialsException(email);
            }

            return tokenService.GenerateToken(account);
        }
    }
}
