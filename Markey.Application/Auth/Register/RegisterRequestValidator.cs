using FluentValidation;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
namespace Markey.Application.Auth.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private readonly MessageManager _messageManager;

    public RegisterRequestValidator(MessageManager messageManager)
    {
        _messageManager = messageManager;

        ValidateMandatory();
        ValidatePhoto();

    }

    private void ValidateMandatory()
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.Password)));

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.Email)));

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.FullName)));


        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.PhoneNumber)));

        RuleFor(x => x.OccupationId)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.OccupationId)));

        RuleFor(x => x.UserName)
    .NotEmpty()
    .WithMessage(_messageManager.GetNotification(
        FunctionalMessages.NOT_NULL, nameof(RegisterRequest.UserName)));

    }

    private void ValidatePhoto()
    {
        RuleFor(x => x.Photo)
            .NotNull()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(RegisterRequest.Photo)))

            .Must(file =>
            {
                if (file == null)
                {
                    return false;
                }

                var allowedTypes = new[] { "image/jpeg", "image/png" };
                return allowedTypes.Contains(file.ContentType);
            })
            .WithMessage("La foto debe ser JPG o PNG.")

            .Must(file =>
            {
                if (file == null) return false;
                const long maxSizeInBytes = 15 * 1024 * 1024;
                return file.Length <= maxSizeInBytes;
            })
            .WithMessage("La foto no debe superar los 15 MB.");
    }
}