using Common.Enumerations;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.CreditCard
{
    public class CreateController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/credit-card/create")]
        public async Task<IActionResult> Create([FromForm] CreateControllerModel model, CancellationToken cancellationToken)
        {
            var card = await dataContext.CreditCards
                .SingleOrDefaultAsync(x => x.Number == model.Number, cancellationToken);

            if(card != null)
            {
                return BadRequest("!کارت ورودی تکراری یا نامعتبر است");
            }

            var cardAccount = await dataContext.Accounts
                .SingleOrDefaultAsync(x => x.AccountId == model.AccountId, cancellationToken);


            dataContext.CreditCards.Add(new Domain.Models.CreditCard
            {
                Account = cardAccount,
                Number = model.Number,
                Income = 0,
                Expense = 0
            });

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
