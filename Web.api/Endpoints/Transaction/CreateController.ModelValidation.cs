using FluentValidation;
using System.Security.Cryptography.Xml;

namespace Web.api.Endpoints.Transaction
{
    public class CreateControllerModelValidation : AbstractValidator<CreateControllerModel>
    {
        public CreateControllerModelValidation()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(100)
                .WithMessage("مبلغ تراکنش باید حداقل 100 باشد");

            RuleFor(x => x.CreationDate).NotEmpty()
                .WithMessage("تاریخ تراکنش نمی تواند خالی باشد");

            RuleFor(x => x.Type).NotEmpty()
                .WithMessage("نوع تراکنش نمی تواند خالی باشد");
        }
    }
}
