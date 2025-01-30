using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Bank
{
    public class ItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int Id { get; set; }
    }
}
