using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class DeleteControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int Id { get; set; }
    }
}
