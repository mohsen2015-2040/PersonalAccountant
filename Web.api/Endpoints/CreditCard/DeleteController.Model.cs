using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.CreditCard
{
    public class DeleteControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CreditCardId { get; set; }
    }
}
