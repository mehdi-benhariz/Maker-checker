using FluentValidation;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.data.Validators
{
    public class RequestValidator : AbstractValidator<Request>
    {
        enum requestStatus
        {
            Pending,
            Approved,
            Rejected
        }
        public RequestValidator()
        {
            // RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Description).MaximumLength(120).When(request => !string.IsNullOrEmpty(request.Description));
            RuleFor(request => request.Status).Must(isValidStatus).WithMessage("Status must be one of the following: Pending, Approved, Rejected");
            RuleFor(request => request.Amount).GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0");
        }

        private bool isValidStatus(string status)
        {
            return Enum.TryParse(status, out requestStatus result);
        }
        private bool isValidText(string text)
        {
            return !string.IsNullOrEmpty(text);
        }
    }


}