using Microsoft.AspNetCore.Mvc;
using Web.Common.CustomBinder;

namespace Web.api.Endpoints.Account
{
    public class ChangePasswordControllerModel
    {
        [ModelBinder(typeof(ProtectedValueBinder))]
        public int AccountId { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string NewConfirmPassword { get; set; }
    }
}
