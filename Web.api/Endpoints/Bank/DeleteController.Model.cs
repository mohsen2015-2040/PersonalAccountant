//using Infrastructure.DataType;
using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Bank
{
    
    public class DeleteControllerModel
    {
        [ModelBinder(binderType: typeof(Web.Common.CustomBinder.ProtectedValueBinder))]
        public int Id { get; set; }
    }
}
