using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.Category
{
    public class ListController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/category/list")]
        public async Task<IActionResult> List([FromQuery]ListControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.Categories
                .Where(x => (x.AccountId == null || x.AccountId == model.AccountId) 
                && x.TransactionTypeId == model.TransactionTypeId)
                .OrderBy(x => x.AccountId)
                .Select(x => new
                {
                    CategoryId = dataProtecting.ProtectData(x.CategoryId),
                    x.Title,
                 //   x.Icon
                }).ToListAsync(cancellationToken);

            return Ok(result);
        }
    }
}
