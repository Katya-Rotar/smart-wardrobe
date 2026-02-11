using BLL.DTO.User;
using FluentValidation;

namespace BLL.Validation
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Matches(@"^[a-zA-Z0-9_]{3,20}$").WithMessage("Username must be 3-20 characters and contain only letters, numbers, or underscores.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email is required.")
                .Matches(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$").WithMessage("Email must be in a valid format.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

            RuleFor(x => x.ProfileImage)
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .When(x => !string.IsNullOrEmpty(x.ProfileImage))
                .WithMessage("Profile image must be a valid URL.");
        }
    }
}
