using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class ItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CategorieId { get; set; }
    }
}
