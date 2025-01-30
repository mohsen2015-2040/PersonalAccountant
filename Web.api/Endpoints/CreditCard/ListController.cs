using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.CreditCard
{
    public class ListController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/credit-card/list")]
        public async Task<IActionResult> List([FromQuery]ListControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.CreditCards
                .Where(x => x.AccountId == model.AccountId)
                .Select(x => new
                {
                    CreditCardId = dataProtecting.ProtectData(x.CreditCardId),
                    x.Number,
                }).ToListAsync(cancellationToken);

            return Ok(result);
        }
    }
}
