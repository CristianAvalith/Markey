using FluentValidation;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;

namespace Markey.Application.User.UpdatePhoto;

public class UpdateUserPhotoRequestValidator : AbstractValidator<UpdateUserPhotoRequest>
{
    private readonly MessageManager _messageManager;

    public UpdateUserPhotoRequestValidator(MessageManager messageManager)
    {
        _messageManager = messageManager;

        ValidateMandatory();
        ValidatePhoto();
    }

    private void ValidateMandatory()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserPhotoRequest.UserId)));
    }

    private void ValidatePhoto()
    {
        RuleFor(x => x.Photo)
            .NotNull()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserPhotoRequest.Photo)))

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