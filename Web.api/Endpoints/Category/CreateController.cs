using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Category
{
    public class CreateController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin,user")]
        [HttpPost("api/category/Create")]
        public async Task<IActionResult> Create([FromForm]CreateControllerModel model, CancellationToken cancellationToken)
        {
                  var account = await dataContext.Accounts
                    .SingleOrDefaultAsync(x => x.AccountId == model.AccountId, cancellationToken);

            var transactionType = await dataContext.TransactionTypes
                .SingleOrDefaultAsync(x => x.TransactionTypeId == model.TransactionTypeId, cancellationToken);
          

            dataContext.Categories.Add(new Domain.Models.Category
            {
                Title = model.Title,
                //Icon = model.Icon,
                Account = account,
                TransactionType = transactionType
            });

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
