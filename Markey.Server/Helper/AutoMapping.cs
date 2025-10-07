using AutoMapper;
using Markey.Application.Auth.Login;
using Markey.Application.Auth.Register;
using Markey.Application.User.GetUser;
using Markey.Application.User.Update;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.DTOs;
using Markey.Server.Controllers.Auth.Request;
using Markey.Server.Controllers.User.Request;

namespace Markey.Server.Helper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        ModelToRequest();
        RequestToDTO();
        DTOToEntity();
        ModelToDTO();
        EntityToDTO();
        DTOToResponse();
    }

    private void ModelToRequest()
    {
        CreateMap<RegisterRequestModel, RegisterRequest>();
        CreateMap<LoginRequestModel, LoginRequest>();
        CreateMap<UpdateUserRequestModel, UpdateUserRequest>();
    }

    private void ModelToDTO()
    {
        CreateMap<RegisterRequest, UserData>();
        CreateMap<LoginRequest, LoginData>();
        CreateMap<UpdateUserRequest, UserDataToUpdate>();
    }

    private void RequestToDTO()
    {
        CreateMap<UserDataToUpdate, User>();

    }

    private void DTOToEntity()
    {
        CreateMap<UserData, User>();

    }

    private void EntityToDTO()
    {
        CreateMap<User, UserData>();
        CreateMap<User, ResumeUserData>();
        CreateMap<User, GetUserData>();
        CreateMap<Occupation, OccupationData>();
        CreateMap<UserDataToUpdate, UserData>();
        CreateMap<UserDataToUpdate, User>();

    }

    private void DTOToResponse()
    {
        CreateMap<GetUserData, GetUserResponse>();
        CreateMap<GetUserData, UpdateUserResponse>();

    }
}
