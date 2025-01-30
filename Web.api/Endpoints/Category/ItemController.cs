using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contract;

namespace Web.api.Endpoints.Category
{
    public class ItemController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpGet("api/category/item")]
        public async Task<IActionResult> Item([FromForm]ItemControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.Categories.Where(x => x.CategoryId == model.CategorieId)
                .Select(x => new
                {
                    CategorieId = dataProtecting.ProtectData(x.CategoryId),
                    x.Title,
               //     x.Icon
                }).ToListAsync(cancellationToken);

            return Ok(result);
        }
    }
}
