using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.CreditCard
{
    public class EditController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/credit-card/edit")]
        public async Task<IActionResult> Edit([FromForm]EditControllerModel model, CancellationToken cancellationToken)
        {
            var modelToEdit = await dataContext.CreditCards
                .SingleOrDefaultAsync(x => x.CreditCardId == model.CreditCardId, cancellationToken);

            if(modelToEdit == null)
            {
                return BadRequest("!ورودی نامعتبر");
            }

            modelToEdit.Number = model.Number;

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
