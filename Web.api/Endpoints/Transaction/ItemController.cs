using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.Transaction
{
    public class ItemController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/transaction/item")]
        public async Task<IActionResult> Item([FromForm]ItemControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.Transactions.AsNoTracking()
                .Where(x => x.TransactionId == model.TransactionId)
                .Select(x => new
                {
                    x.Amount,
                    CreditCardNumber = x.CreditCard.Number,
                    x.CreationDate,
                   // x.Type,
                    CategoryTitle = x.Category.Title,
                    x.Description,
                }).FirstAsync(cancellationToken);

            return Ok(result);
        }
    }
}
