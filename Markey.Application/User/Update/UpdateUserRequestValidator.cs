using FluentValidation;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
namespace Markey.Application.User.Update;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    private readonly MessageManager _messageManager;

    public UpdateUserRequestValidator(MessageManager messageManager)
    {
        _messageManager = messageManager;
        ValidateUser();
    }

    private void ValidateUser()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .When(x => x.Id != Guid.Empty)
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserRequest.Id)));

        RuleFor(x => x.OccupationId)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserRequest.OccupationId)));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserRequest.PhoneNumber)));

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(UpdateUserRequest.FullName)));
    }
}