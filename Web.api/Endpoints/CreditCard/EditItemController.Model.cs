using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.CreditCard
{
    public class EditItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CreditCardId { get; set; }
    }
}
