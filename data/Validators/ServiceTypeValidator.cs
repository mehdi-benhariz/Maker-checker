using FluentValidation;
using maker_checker_v1.models.entities;

namespace maker_checker_v1.data.Validators
{
    public class ServiceTypeValidator : AbstractValidator<ServiceType>
    {
        public ServiceTypeValidator()
        {
            // RuleFor(serviceType => serviceType.Id).NotEmpty();
            RuleFor(serviceType => serviceType.Name).NotEmpty().Length(3, 50).WithMessage("Name must be between 3 and 50 characters");
        }
    }

}