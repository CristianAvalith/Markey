using AutoMapper;
using FluentValidation;
using Markey.Domain.Interfaces;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.ResponseData;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Messages;

namespace Markey.Application.User.UpdatePhoto;

public class UpdateUserPhotoHandler : IRequestHandler<UpdateUserPhotoRequest, ResponseData<UpdateUserPhotoResponse, ResponseMessage>>
{
    private readonly IUserService _userService;
    private readonly IValidator<UpdateUserPhotoRequest> _validator;
    private readonly MessageManager _messageManager;

    public UpdateUserPhotoHandler(IUserService userService, IValidator<UpdateUserPhotoRequest> validator, MessageManager messageManager)
    {
        _userService = userService;
        _validator = validator;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<UpdateUserPhotoResponse, ResponseMessage>> Handle(UpdateUserPhotoRequest request)
    {
        UpdateUserPhotoResponse response = null;

        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            var photoUrl = await _userService.UpdateUserPhotoAsync(request.UserId, request.Photo);

            var responseData = new ResponseData<UpdateUserPhotoResponse, ResponseMessage>(
                               code: (int)Code.SUCCESS,
                               data: new UpdateUserPhotoResponse { PhotoUrl = photoUrl },
                               message: null);

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }
        catch (UserNotFoundException e)
        {
            return await HandleException(Code.NOT_FOUND, [_messageManager.GetNotification(FunctionalMessages.USER_NOT_FOUND)], e, response);
        }
        catch (Exception e)
        {
            return await HandleException(Code.SERVICE_UNAVAILABLE, [e.Message], e, null);
        }
    }

    private async Task<ResponseData<UpdateUserPhotoResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e, UpdateUserPhotoResponse response = null)
    {
        var responseData = new ResponseData<UpdateUserPhotoResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: response,
                           message: message);

        return await Task.FromResult(responseData);
    }
}