using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Transaction
{
    public class EditItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int TransactionId { get; set; }
    }
}
