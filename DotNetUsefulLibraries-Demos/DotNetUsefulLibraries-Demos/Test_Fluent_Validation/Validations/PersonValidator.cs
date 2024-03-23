using FluentValidation;
using Test_Fluent_Validation.Models;

namespace Test_Fluent_Validation.Validations;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id have to be not null.");
        RuleFor(x => x.Name).Length(0, 10).WithMessage("Name is long.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email."); ;
        RuleFor(x => x.Age).InclusiveBetween(18, 60);
    }
}
