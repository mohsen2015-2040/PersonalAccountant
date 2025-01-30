using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Category
{
    public class EditItemController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpGet("api/category/edit")]
        public async Task<IActionResult> EditItem([FromForm]EditItemControllerModel model, CancellationToken cancellationToken)
        {
            var result = await dataContext.Categories.Where(x => x.CategoryId == model.Id)
                .Select(x => new
                {
                    x.CategoryId,
                    x.Title,
                //    x.Icon
                }).FirstAsync(cancellationToken);
           
            return Ok(result);
        }
    }
}
