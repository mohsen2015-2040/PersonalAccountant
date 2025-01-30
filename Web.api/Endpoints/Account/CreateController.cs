using Common.Enumerations;
using Domain;
using Domain.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;
using Services.Implementation;

namespace Web.api.Endpoints.Account
{
    [ApiController]
    public class CreateController(DatabaseContext dataContext, IJwtTokenGenerator tokenGenerator, IDataProtecting dataProtecting) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("api/account/create")]
        public async Task<IActionResult> Create([FromForm] CreateControllerModel model, CancellationToken cancellationToken)
        {
            var username = await dataContext.Accounts
                .FirstOrDefaultAsync(x => x.Username == model.Username, cancellationToken);

            if (username != null)
            {
                return BadRequest("این نام کاربری قبلا ثبت نام شده است");
            }

            var accountToAdd = new Domain.Models.Account
            {
                Username = model.Username,
                //Password = model.Password,
                RoleId = (int)RoleEnumeration.user
            };

            accountToAdd.Password = PasswordHashing.HashPassword(accountToAdd, model.Password);

            var entityEntry = dataContext.Accounts.Add(accountToAdd);

            await dataContext.SaveChangesAsync(cancellationToken);

            var accountId = dataProtecting.ProtectData(entityEntry.Entity.AccountId);

            var token = tokenGenerator.GenerateToken(model.Username, RoleEnumeration.user.ToString(), accountId);

            return Ok(new
            {
                Token = token,
                Account = new
                {
                    accountId
                }
            });
        }
    }
}
