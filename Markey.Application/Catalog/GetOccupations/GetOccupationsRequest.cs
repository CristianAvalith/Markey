using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;
namespace Markey.Application.Catalog.GetOccupations;

public class GetOccupationsRequest : IRequest<ResponseData<GetOccupationsResponse, ResponseMessage>>
{
}