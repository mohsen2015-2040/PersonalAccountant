using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Transaction
{
    public class DeleteController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpPost("api/transaction/delete")]
        public async Task<IActionResult> Delete([FromForm]DeleteControllerModel model, CancellationToken cancellationToken)
        {
            await dataContext.Transactions.Where(x => x.TransactionId == model.TransactionId)
                .ExecuteDeleteAsync(cancellationToken);

            return Ok();
        }
    }
}
