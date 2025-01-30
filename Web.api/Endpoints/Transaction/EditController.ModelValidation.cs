using FluentValidation;

namespace Web.api.Endpoints.Transaction
{
    public class EditControllerModelValidation : AbstractValidator<EditControllerModel>
    {
        public EditControllerModelValidation()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(100)
                .WithMessage("مبلغ تراکنش باید حداقل 100 باشد");

            RuleFor(x => x.CreationDate).NotEmpty()
                .WithMessage("تاریخ تراکنش نمی تواند خالی باشد");

            RuleFor(x => x.TransactionTypeId).NotEmpty()
                .WithMessage("نوع تراکنش نمی تواند خالی باشد");
        }
    }
}
