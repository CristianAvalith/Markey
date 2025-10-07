using FluentValidation;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
namespace Markey.Application.User.GetUser;

public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    private readonly MessageManager _messageManager;

    public GetUserRequestValidator(MessageManager messageManager)
    {
        _messageManager = messageManager;

        ValidateMandatory();
    }

    private void ValidateMandatory()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(_messageManager.GetNotification(
                FunctionalMessages.NOT_NULL, nameof(GetUserRequest.UserId)));
    }
}