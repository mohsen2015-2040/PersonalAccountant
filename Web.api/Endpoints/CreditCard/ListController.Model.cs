using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.CreditCard
{
    public class ListControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }
    }
}
