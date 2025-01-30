using FluentValidation;

namespace Web.api.Endpoints.Account
{
    public class ChangePasswordControllerModelValidation : AbstractValidator<ChangePasswordControllerModel>
    {
        public ChangePasswordControllerModelValidation()
        {
            RuleFor(x => x.NewPassword).MinimumLength(4)
                .WithMessage("طول رمز عبور باید حداقل 4 کاراکتر باشد");

            RuleFor(x => x.NewConfirmPassword).Equal(x => x.NewPassword)
                .WithMessage("فیلدهای رمز عبور با هم منطبق نیستند");
        }
    }
}
