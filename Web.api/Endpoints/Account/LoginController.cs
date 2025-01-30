using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;
using Services.Implementation;
using Web.Common;

namespace Web.api.Endpoints.Account
{
    [ApiController]
    public class LoginController(DatabaseContext dataContext, IJwtTokenGenerator tokenGenerator,
        IDataProtecting dataProtecting) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("api/account/login")]
        public async Task<IActionResult> Login([FromForm] LoginControllerModel model, CancellationToken cancellation)
        {
            var accountToLogin = await dataContext.Accounts
                .SingleOrDefaultAsync(x => x.Username == model.Username, cancellation);

            if (accountToLogin == null)
            {
                return BadRequest("!این نام کاربری ثبت نام نشده است");
            }

            if(!PasswordHashing.VerifyPassword(accountToLogin, accountToLogin.Password, model.Password))
            {
                return BadRequest("!رمز عبور اشتباه است");
            }

            var accountId = dataProtecting.ProtectData(accountToLogin.AccountId);
            var token = tokenGenerator.GenerateToken(accountToLogin.Username, accountToLogin.Role.Name, accountId);

            return Ok(new
            {
                token,
                Account = new
                {
                    accountId
        }
            });
        }
    }
}
