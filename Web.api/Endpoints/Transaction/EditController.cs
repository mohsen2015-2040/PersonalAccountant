using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.Transaction
{
    public class EditController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/transaction/edit")]
        public async Task<IActionResult> Edit([FromForm] EditControllerModel model, CancellationToken cancellationToken)
        {
            var modelToEdit = await dataContext.Transactions
                 .SingleOrDefaultAsync(x => x.TransactionId == model.TransactionId, cancellationToken);

            if (modelToEdit == null)
            {
                return BadRequest("!ورودی نامعتبر");
            }

            modelToEdit.Amount = model.Amount;

            modelToEdit.Description = model.Description;

            modelToEdit.CreationDate = DateOnly.FromDateTime(model.CreationDate);

            modelToEdit.Category = await dataContext.Categories
                .SingleOrDefaultAsync(x => x.CategoryId == model.CategoryId,
                cancellationToken);

            modelToEdit.CreditCard = await dataContext.CreditCards
                .SingleOrDefaultAsync(x => x.CreditCardId == model.CreditCardId,
                cancellationToken);

            modelToEdit.TransactionType = await dataContext.TransactionTypes
                .SingleOrDefaultAsync(x => x.TransactionTypeId == model.TransactionTypeId,
                cancellationToken);


            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
