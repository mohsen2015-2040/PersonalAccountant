using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Transaction
{
    public class ItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int TransactionId { get; set; }
    }
}
