using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Bank
{
    public class DeleteController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpPost("api/bank/delete")]
        public async Task<IActionResult> Delete([FromForm]DeleteControllerModel model, CancellationToken cancellationToken)
        {
         /*   await dataContext.Banks
                .Where(x => x.BankId == model.Id)
                .ExecuteDeleteAsync(cancellationToken);*/

            return Ok();
        }
    }
}
