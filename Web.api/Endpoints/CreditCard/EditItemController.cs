using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.CreditCard
{
    public class EditItemController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/credit-card/edit")]
        public async Task<IActionResult> EditItem([FromForm]EditItemControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.CreditCards
                .Where(x => x.CreditCardId == model.CreditCardId)
                .Select(x => new
                {
                    CreditCardId = dataProtecting.ProtectData(x.CreditCardId),
                    x.Number
                }).FirstAsync(cancellationToken);

            return Ok(result);
        }
    }
}
