using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Implementation;

namespace Web.api.Endpoints.Account
{
    [ApiController]
    public class ChangePasswordController(DatabaseContext dataContext) : ControllerBase
    {
        [HttpPost("api/account/change-password")]
        public async Task<IActionResult> ChangePassword([FromForm]ChangePasswordControllerModel model, CancellationToken cancellationToken)
        {
            var account = await dataContext.Accounts
                .SingleAsync(x => x.AccountId == model.AccountId, cancellationToken);

            if(!PasswordHashing.VerifyPassword(account, account.Password, model.Password))
            {
                return BadRequest("!رمز عبور اشتباه است");
            }

            account.Password = PasswordHashing.HashPassword(account, model.NewPassword);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
