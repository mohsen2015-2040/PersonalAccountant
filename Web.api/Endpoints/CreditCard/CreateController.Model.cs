using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.CreditCard
{
    public class CreateControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }

        //  [ModelBinder(typeof(ProtectedValueBinder))]
        //  public int BankId { get; set; }

        public string Number { get; set; } = "";

      //  public long Income { get; set; }

      //  public long Expense { get; set; }
    }
}
