using FluentValidation;
using Markey.Application.Auth.Login;
using Markey.CrossCutting.Excepciones;
using Markey.CrossCutting.Helpers;
using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.Messages;
using Markey.CrossCutting.MessagesManager;
using Markey.CrossCutting.ResponseData;
using Markey.Domain.Interfaces;

namespace Markey.Application.Catalog.GetOccupations;

public class GetOccupationsHandler : IRequestHandler<GetOccupationsRequest, ResponseData<GetOccupationsResponse, ResponseMessage>>
{
    private readonly ICatalogService _catalogService;
    private readonly MessageManager _messageManager;

    public GetOccupationsHandler(ICatalogService catalogService,
                                MessageManager messageManager)
    {
        _catalogService = catalogService;
        _messageManager = messageManager;
    }

    public async Task<ResponseData<GetOccupationsResponse, ResponseMessage>> Handle(GetOccupationsRequest request)
    {
        try
        {
            var occupations = await _catalogService.GetOccupationsAsync();

            var response = new GetOccupationsResponse() { Occupations = occupations };

            var responseData = new ResponseData<GetOccupationsResponse, ResponseMessage>(
                               code: (int)Code.SUCCESS,
                               data: response,
                               message: null
                               );

            return responseData;
        }
        catch (Exception)
        {
            var responseData = new ResponseData<GetOccupationsResponse, ResponseMessage>(
                               code: (int)Code.SERVICE_UNAVAILABLE,
                               data: null,
                               message: [_messageManager.GetNotification(FunctionalMessages.SERVICE_UNAVAILABLE)]);


            return await Task.FromResult(responseData);
        }
    }
}