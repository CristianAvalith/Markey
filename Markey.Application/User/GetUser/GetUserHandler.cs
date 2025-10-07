using AutoMapper;
using FluentValidation;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.ResponseData;
using Markey.Domain.Interfaces;
using Markey.Persistance.DTOs;
namespace Markey.Application.User.GetUser;

public class GetUserHandler : IRequestHandler<GetUserRequest, ResponseData<GetUserResponse, ResponseMessage>>
{
    private readonly IUserService _userService;
    private readonly IValidator<GetUserRequest> _validator;
    private readonly IMapper _mapper;
    private readonly MessageManager _messageManager;

    public GetUserHandler(IUserService userService,
                           IValidator<GetUserRequest> validator,
                           IMapper mapper,
                           MessageManager messageManager)
    {
        _userService = userService;
        _validator = validator;
        _mapper = mapper;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<GetUserResponse, ResponseMessage>> Handle(GetUserRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            GetUserData userData = await _userService.GetUserByIdAsync(request.UserId);

            var responseData = new ResponseData<GetUserResponse, ResponseMessage>(
                               code: (int)Code.SUCCESS,
                               data: _mapper.Map<GetUserResponse>(userData),
                               message: null);

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }
        catch (UserNotFoundException e)
        {
            return await HandleException(Code.CONFLICT, [e.Error], e);
        }
        catch (Exception e)
        {
            return await HandleException(Code.SERVICE_UNAVAILABLE, [_messageManager.GetNotification(FunctionalMessages.SERVICE_UNAVAILABLE)], e);
        }
    }

    private async Task<ResponseData<GetUserResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e)
    {
        var responseData = new ResponseData<GetUserResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: null,
                           message: message);

        return await Task.FromResult(responseData);
    }
}
