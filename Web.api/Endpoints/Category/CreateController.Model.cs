using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class CreateControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }

        public int TransactionTypeId { get; set; }

        public string Title { get; set; } = "";

        public string Icon { get; set; } = "";
       
    }
}
