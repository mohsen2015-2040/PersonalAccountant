using FluentValidation;

namespace Web.api.Endpoints.CreditCard
{
    public class CreateControllerModelValidation : AbstractValidator<CreateControllerModel>
    {
        public CreateControllerModelValidation()
        {
            RuleFor(x => x.Number).NotEmpty().Length(16)
                .WithMessage("شمارت کارت باید 16 رقم باشد");
        }
    }
}
