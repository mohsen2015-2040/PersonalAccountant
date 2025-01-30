using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Transaction
{
    public class CreateController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/transaction/create")]
        public async Task<IActionResult> Create([FromForm] CreateControllerModel model, CancellationToken cancellationToken)
        {
            var transactionCard = await dataContext.CreditCards
                .SingleOrDefaultAsync(x => x.CreditCardId == model.CreditCardId, cancellationToken);

            var transactionCategory = await dataContext.Categories
                .SingleOrDefaultAsync(x => x.CategoryId == model.CategoryId, cancellationToken);

            var transactionType = await dataContext.TransactionTypes
                .SingleOrDefaultAsync(x => x.TransactionTypeId == model.TransactionTypeId, cancellationToken);
            
            if (transactionCard == null || transactionType == null || transactionCategory == null)
            {
                return BadRequest("!ورودی نامعتبر");
            }

            dataContext.Add(new Domain.Models.Transaction
            {
                CreditCard = transactionCard,
                Category = transactionCategory,
                TransactionType = transactionType,
                Amount = model.Amount,
                Description = model.Description,
                CreationDate = DateOnly.FromDateTime(model.CreationDate)
            });

            if (transactionType.TransactionTypeId == 1)
            {
                transactionCard.Income += model.Amount;
            }
            else if (transactionType.TransactionTypeId == 2)
            {
                transactionCard.Expense += model.Amount;
            }

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
