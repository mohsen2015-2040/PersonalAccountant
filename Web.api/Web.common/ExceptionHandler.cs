using Microsoft.AspNetCore.Mvc.Filters;
using Domain;
using Domain.Domain.Models;

namespace Web.Common
{
    public class ExceptionHandler(DatabaseContext dataContext) : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            
            dataContext.ErrorLogs.Add(new ErrorLog
            {
                Address = context.ActionDescriptor.DisplayName,
                CreationDate = DateTime.Now,
                Message = context.Exception.Message,
            });

            await dataContext.SaveChangesAsync();
        }
    }
}
