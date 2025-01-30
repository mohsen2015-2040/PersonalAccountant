using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Web.api.Endpoints.Bank
{
    public class ItemController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpGet("api/bank/item")]
        public async Task<IActionResult> Item([FromForm]ItemControllerModel model, CancellationToken cancellationToken)
        {
          /*  var result = await dataContext.Banks
                .SingleOrDefaultAsync(x => x.BankId == model.Id, cancellationToken);*/

            return Ok();
        }
    }
}
