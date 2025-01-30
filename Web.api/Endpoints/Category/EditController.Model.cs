using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class EditControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CategorieId { get; set; }

        public string Title { get; set; } = "";

        public string Icon { get; set; } = "";
    }
}
