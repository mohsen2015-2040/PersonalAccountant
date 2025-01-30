using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.CreditCard
{
    public class EditControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CreditCardId { get; set; }

        public string Number { get; set; } = "";
    }
}
