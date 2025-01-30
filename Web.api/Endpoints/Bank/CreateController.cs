using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.api.Endpoints.Bank
{
    public class CreateController(DatabaseContext dataContext) : Controller
    {
        [Authorize(Roles = "admin")]
        [HttpPost("api/bank/create")]
        public async Task<IActionResult> Create([FromForm] CreateControllerModel model, CancellationToken cancellationToken)
        {
         /*   var modelToAdd = new Domain.Models.Bank
            {
                Code = model.Code,
                Title = model.Title,
            };

            dataContext.Banks.Add(modelToAdd);

            await dataContext.SaveChangesAsync(cancellationToken);*/

            return Ok();
        }
    }
}
