using FluentValidation;

namespace Web.api.Endpoints.Account
{
    public class CreateControllerModelValidation : AbstractValidator<CreateControllerModel>
    {
        public CreateControllerModelValidation()
        {
            RuleFor(x => x.Password).MinimumLength(4)
                .WithMessage("طول رمز عبور باید حداقل 4 کاراکتر باشد");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                .WithMessage("فیلدهای رمز عبور با هم منطبق نیستند");

            RuleFor(x => x.Username).NotEmpty()
                .WithMessage("نام کاربری نمی تواند خالی باشد");
        }
    }
}
