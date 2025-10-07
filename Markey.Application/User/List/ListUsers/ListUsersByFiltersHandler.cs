using FluentValidation;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.ResponseData;
using Markey.Domain.Interfaces;
namespace Markey.Application.User.List.ListUsers;

public class ListUsersByFiltersHandler : IRequestHandler<ListUsersByFiltersRequest, ResponseData<ListUsersByFiltersResponse, ResponseMessage>>
{
    private readonly IUserService _userService;
    private readonly IValidator<ListUsersByFiltersRequest> _validator;
    private readonly MessageManager _messageManager;

    public ListUsersByFiltersHandler(IUserService userService,
                           IValidator<ListUsersByFiltersRequest> validator,
                           MessageManager messageManager)
    {
        _userService = userService;
        _validator = validator;
        _messageManager = messageManager;

    }
    public async Task<ResponseData<ListUsersByFiltersResponse, ResponseMessage>> Handle(ListUsersByFiltersRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            var response = new ListUsersByFiltersResponse() 
            { 
                Users = await _userService.ListUsersByFilterAsync(request.FullName, request.PageSize, request.PageNumber)
            };

            response.Count = response.Users.Count;
            response.PageNumber = request.PageNumber;

            var responseData = new ResponseData<ListUsersByFiltersResponse, ResponseMessage>(
                               code: (int)Code.SUCCESS,
                               data: response,
                               message: null
                               );

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }
        catch (Exception e)
        {
            return await HandleException(Code.SERVICE_UNAVAILABLE, [_messageManager.GetNotification(FunctionalMessages.SERVICE_UNAVAILABLE)], e);
        }
    }

    private async Task<ResponseData<ListUsersByFiltersResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e)
    {
        var responseData = new ResponseData<ListUsersByFiltersResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: null,
                           message: message);

        return await Task.FromResult(responseData);
    }
}
