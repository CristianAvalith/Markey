using AutoMapper;
using FluentValidation;
using Markey.Domain.Interfaces;
using Markey.Persistance.DTOs;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.ResponseData;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Messages;
namespace Markey.Application.Auth.Register;

public class RegisterHandler : IRequestHandler<RegisterRequest, ResponseData<RegisterResponse, ResponseMessage>>
{
    private readonly IUserService _registerService;
    private readonly IValidator<RegisterRequest> _validator;
    private readonly IMapper _mapper;
    private readonly MessageManager _messageManager;

    public RegisterHandler(IUserService registerService, IValidator<RegisterRequest> validator, IMapper mapper, MessageManager messageManager)
    {
        _registerService = registerService;
        _validator = validator;
        _mapper = mapper;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<RegisterResponse, ResponseMessage>> Handle(RegisterRequest request)
    {

        RegisterResponse response = null;

        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            Guid userId = await _registerService.RegisterAsync(_mapper.Map<UserData>(request));

            var responseData = new ResponseData<RegisterResponse, ResponseMessage>(
                               code: (int)Code.CREATED,
                               data: new RegisterResponse { UserId = userId },
                               message: null);

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }
        catch (RegisterException e)
        {
            return await HandleException(Code.CONFLICT, [_messageManager.GetNotification(FunctionalMessages.USER_ALREADY_EXISTS)], e, response);
        }
        catch (Exception e)
        {
            return await HandleException(Code.SERVICE_UNAVAILABLE, [e.Message], e, null);
        }
    }
    private async Task<ResponseData<RegisterResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e, RegisterResponse response = null)
    {

        var responseData = new ResponseData<RegisterResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: response,
                           message: message);

        return await Task.FromResult(responseData);
    }
}
