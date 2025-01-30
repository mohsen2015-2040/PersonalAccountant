using Common.Extentions;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Contract;

namespace Web.api.Endpoints.Transaction
{
    public class ListController(DatabaseContext dataContext, IDataProtecting dataProtecting) : Controller
    {
        [Authorize(Roles = "user,admin")]
        [HttpGet("api/transaction/list")]
        public async Task<IActionResult> List([FromQuery] ListControllerModel model, CancellationToken cancellationToken)
        {
            var transactions = dataContext.Transactions
                .AsNoTracking()
                .Where(x => x.CreditCard.AccountId == model.AccountId)
                .AsQueryable();

            if (model.CreditCardId != 0)
            {
                transactions = transactions.Where(x => x.CreditCardId == model.CreditCardId);
            }

            if (model.TransactionTypeId != 0)
            {
                transactions = transactions.Where(x => x.TransactionTypeId == model.TransactionTypeId);
            }

            switch (model.Term)
            {
                case "day":
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-1)));
                    break;
                case "month":
                    var term = DateTime.Now.GetDayOfMonth();
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-term)));
                    break;
                case "week":
                    term = DateTime.Now.GetDayOfWeek();
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-term)));
                    break;
                case "threeMonth":
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-90)));
                    break;
                case "sixMonth":
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-180)));
                    break;
                case "year":
                    term = DateTime.Now.GetDayOfYear();
                    transactions = transactions
                .Where(x => x.CreationDate > DateOnly.FromDateTime(DateTime.Now.AddDays(-term)));
                    break;
            }

            switch (model.Sort)
            {
                case "amount":
                    transactions = transactions.OrderByDescending(x => x.Amount);
                    break;
                case "date":
                    transactions = transactions.OrderByDescending(x => x.CreationDate);
                    break;
            }

            var income = transactions.Where(x => x.TransactionTypeId == 1).Sum(x => x.Amount);
            var expense = transactions.Where(x => x.TransactionTypeId == 2).Sum(x => x.Amount);
            
            if (model.SequenceLength != 0)
            {
                transactions = transactions
                    .Skip(model.Index)
                    .Take(model.SequenceLength);
            }

            var result = await transactions.Select(x => new
            {
                TransactionId = dataProtecting.ProtectData(x.TransactionId),
                CreditCard = new
                {
                    x.CreditCard.Number,
                    CreditCardId = dataProtecting.ProtectData(x.CreditCard.CreditCardId)
                },
                Category = new
                {
                    x.Category.Title,
                    CategoryId = dataProtecting.ProtectData(x.Category.CategoryId)
                },
                x.TransactionTypeId,
                CreationDate = x.CreationDate.ToPersianDate(),
                x.Amount,
                x.Description
            }).ToListAsync(cancellationToken);

            return Ok(new
            {
                Balance = new
                {
                    income,
                    expense
                },
                Transactions = result
            });
        }
    }
}
