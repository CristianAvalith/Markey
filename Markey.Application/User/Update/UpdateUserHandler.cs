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

namespace Markey.Application.User.Update;

public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, ResponseData<UpdateUserResponse, ResponseMessage>>
{
    private readonly IUserService _userService;
    private readonly IValidator<UpdateUserRequest> _validator;
    private readonly IMapper _mapper;
    private readonly MessageManager _messageManager;


    public UpdateUserHandler(IUserService userService,
                           IValidator<UpdateUserRequest> validator,
                           IMapper mapper,
                           MessageManager messageManager)
    {
        _userService = userService;
        _validator = validator;
        _mapper = mapper;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<UpdateUserResponse, ResponseMessage>> Handle(UpdateUserRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            var userDataRequest = _mapper.Map<UserDataToUpdate>(request);
            userDataRequest.Id = request.Id;

            GetUserData userData = await _userService.PatchUserAsync(userDataRequest);

            var responseData = new ResponseData<UpdateUserResponse, ResponseMessage>(
                               code: (int)Code.SUCCESS,
                               data: _mapper.Map<UpdateUserResponse>(userData),
                               message: null
                               );

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }

        catch (UserNotFoundException e)
        {
            return await HandleException(Code.NOT_FOUND, [e.Error], e);
        }
        catch (Exception e)
        {
            return await HandleException(Code.SERVICE_UNAVAILABLE, [_messageManager.GetNotification(FunctionalMessages.SERVICE_UNAVAILABLE)], e);
        }
    }

    private async Task<ResponseData<UpdateUserResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e)
    {
        var responseData = new ResponseData<UpdateUserResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: null,
                           message: message);

        return await Task.FromResult(responseData);
    }
}
