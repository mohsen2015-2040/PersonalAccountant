using Common.Extentions;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;
using System.Globalization;

namespace Web.api.Endpoints.Transaction
{
    public class EditItemController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/transaction/edit")]
        public async Task<IActionResult> EditItem([FromQuery] EditItemControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.Transactions
                .Where(x => x.TransactionId == model.TransactionId)
                .Select(x => new
                {
                    TransactionId = dataProtecting.ProtectData(x.TransactionId),
                    x.Amount,
                    CreationDate = x.CreationDate.ToPersianDate(),
                    x.Description,
                    Category = new 
                    {
                        CategoryId = dataProtecting.ProtectData(x.Category.CategoryId),
                        x.Category.Title
                    },
                    CreditCard = new
                    {
                        CreditCardId = dataProtecting.ProtectData(x.CreditCard.CreditCardId),
                        x.CreditCard.Number
                    },
                    x.TransactionType.TransactionTypeId,
                }).FirstAsync(cancellationToken);

            return Ok(result);
        }
    }
}
