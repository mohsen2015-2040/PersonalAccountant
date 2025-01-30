using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.CreditCard
{
    public class DeleteController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/credit-card/delete")]
        public async Task<IActionResult> Delete([FromForm]DeleteControllerModel model, CancellationToken cancellationToken)
        {
            await dataContext.CreditCards
                .Where(x => x.CreditCardId == model.CreditCardId)
                .ExecuteDeleteAsync(cancellationToken);

            return Ok();
        }
    }
}
