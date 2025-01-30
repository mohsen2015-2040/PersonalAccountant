using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class EditItemControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int Id { get; set; }
    }
}
