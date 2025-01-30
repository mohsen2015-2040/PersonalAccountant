using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Transaction
{
    public class ListControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int CreditCardId { get; set; }

        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }

        public int TransactionTypeId { get; set; }

        public string Term { get; set; } = "day";

        public string Sort { get; set; } = "date";

        public int Index { get; set; }

        public int SequenceLength { get; set; }
    }
}
