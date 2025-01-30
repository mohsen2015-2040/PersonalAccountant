using FluentValidation;

namespace Web.api.Endpoints.Account
{
    public class LoginControllerModelValidation : AbstractValidator<LoginControllerModel>
    {
        public LoginControllerModelValidation()
        {
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("طول رمز باید حداقل 4 کاراکتر باشد");

            RuleFor(x => x.Username).NotEmpty().WithMessage("نام کاربری نمی تواند ");
        }
    }
}
