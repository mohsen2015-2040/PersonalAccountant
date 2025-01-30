
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Services.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Web.api.Endpoints.Account
{

    [ApiController]
    public class CheckAccessController(DatabaseContext dataContext, IDataProtecting dataProtecting, IConfiguration configuration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("api/account/check-access")]
        public async Task<IActionResult> CheckAccess(CancellationToken cancellationToken)
        {
            try
            {
                var token = HttpContext.Request.Headers.Authorization.ToString();

                var accountId = await ValidateToken(token, cancellationToken);

                return Ok(accountId);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [NonAction]
        private async Task<string> ValidateToken(string token, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new Exception();
            }
            token = token[7..];

            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtAudience = configuration.GetSection("Jwt:Audience").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();
            TokenValidationParameters tokenParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken validatedToken);

            var validatedJwt = (JwtSecurityToken)validatedToken;

            var idProtected = validatedJwt.Claims.First(x => x.Type == "nameid").Value;

            var idBytes = dataProtecting.UnprotectData(idProtected);

            int accountId = BitConverter.ToInt32(idBytes);

            var validatedAccount = await dataContext.Accounts
                .SingleOrDefaultAsync(x => x.AccountId == accountId, cancellationToken);

            if (validatedAccount == null)
            {
                throw new Exception();
            }

            return idProtected;
        }
    }
}
