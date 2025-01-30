using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Category
{
    public class ListControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }

        public int TransactionTypeId { get; set; }
    }
}
