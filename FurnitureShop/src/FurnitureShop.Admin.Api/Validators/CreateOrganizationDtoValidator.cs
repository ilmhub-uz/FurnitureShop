using FluentValidation;
using FurnitureShop.Admin.Api.Dtos;

namespace FurnitureShop.Admin.Api.Validators
{
    public class CreateOrganizationDtoValidator : AbstractValidator<CreateOrganizationDto>
    {
        public CreateOrganizationDtoValidator()
        {
            RuleFor(createorganizationdto => createorganizationdto.Name).NotNull();
            RuleFor(createorganizationdto => createorganizationdto.Name)
           .MaximumLength(25).MinimumLength(3).When(createorganizationdto => createorganizationdto.Name is not null);


        }
    }
}
