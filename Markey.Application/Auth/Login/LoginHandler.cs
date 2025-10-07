using Markey.CrossCutting.ResponseData;
using Markey.CrossCutting.Mediator;
using FluentValidation;
using AutoMapper;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Helpers;
using Markey.Domain.Interfaces;
using Markey.Domain.Models;
using Markey.Persistance.DTOs;
using Markey.CrossCutting.Messages;
namespace Markey.Application.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest, ResponseData<LoginResponse, ResponseMessage>>
{
    private readonly ILoginService _loginService;
    private readonly IValidator<LoginRequest> _validator;
    private readonly IMapper _mapper;
    private readonly MessageManager _messageManager;

    public LoginHandler(ILoginService loginService, IValidator<LoginRequest> validator, IMapper mapper, MessageManager messageManager)

    {
        _loginService = loginService;
        _validator = validator;
        _mapper = mapper;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<LoginResponse, ResponseMessage>> Handle(LoginRequest request)
    {

        TokenResponse authAccess = null;

        try
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ParameterException(errorMessages);
            }

            authAccess = await _loginService.LoginAsync(_mapper.Map<LoginData>(request));

            var responseData = new ResponseData<LoginResponse, ResponseMessage>(
                                   code: (int)Code.SUCCESS,
                                   data: new LoginResponse() { AccessToken = authAccess.AccessToken },
                                   message: null
                                   );

            return await Task.FromResult(responseData);
        }
        catch (ParameterException e)
        {
            return await HandleException(Code.BAD_REQUEST, e.Errors, e);
        }
        catch (IncorrectCredentialException e)
        {
            return await HandleException(Code.UNAUTHORIZED, [e.Error], e);
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

    private async Task<ResponseData<LoginResponse, ResponseMessage>> HandleException(Code errorCode, List<string> message, Exception e)
    {

        var responseData = new ResponseData<LoginResponse, ResponseMessage>(
                           code: (int)errorCode,
                           data: null,
                           message: message);


        return await Task.FromResult(responseData);
    }
}
