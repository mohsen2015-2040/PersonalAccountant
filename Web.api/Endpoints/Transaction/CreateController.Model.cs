using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Transaction
{
    public class CreateControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CreditCardId { get; set; }

        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CategoryId { get; set; }

        public int TransactionTypeId { get; set; }

        public long Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Description { get; set; }

        public int Type { get; set; }
    }
}
