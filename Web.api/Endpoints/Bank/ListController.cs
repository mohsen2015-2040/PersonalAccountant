using Domain;
using Microsoft.AspNetCore.Authorization;

//using Infrastructure.DataType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.Bank
{
    public class ListController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/bank/list")]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
         /*   var result = await dataContext.Banks
                .AsNoTracking().Select(x => new
            {
                BankId = dataProtecting.ProtectData(x.BankId),
                x.Code,
                x.Title
            }).ToListAsync(cancellationToken);*/

            return Ok();
        }
    }
}
