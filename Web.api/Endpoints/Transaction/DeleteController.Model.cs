using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Transaction
{
    public class DeleteControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int TransactionId { get; set; }
    }
}
