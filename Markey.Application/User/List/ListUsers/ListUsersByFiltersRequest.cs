using Markey.CrossCutting.Mediator;
using Markey.CrossCutting.ResponseData;

namespace Markey.Application.User.List.ListUsers;
public class ListUsersByFiltersRequest : IRequest<ResponseData<ListUsersByFiltersResponse, ResponseMessage>>
{
    public string FullName { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}
