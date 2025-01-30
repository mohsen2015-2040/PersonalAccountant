using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.api.Endpoints.Category
{
    public class EditController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpPost("api/category/edit")]
        public async Task<IActionResult> Edit([FromForm] EditControllerModel model, CancellationToken cancellationToken)
        {
            var modelToEdit = await dataContext.Categories
                .SingleOrDefaultAsync(x => x.CategoryId == model.CategorieId, cancellationToken);

            modelToEdit.Title = model.Title;
           // modelToEdit.Icon = model.Icon;

            await dataContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
