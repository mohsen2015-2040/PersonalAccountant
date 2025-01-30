using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Category
{
    public class DeleteController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpPost("api/category/delete")]
        public async Task<IActionResult> Delete([FromForm]DeleteControllerModel model, CancellationToken cancellationToken)
        {
            await dataContext.Categories
                .Where(x => x.CategoryId == model.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return Ok();
        }
    }
}
