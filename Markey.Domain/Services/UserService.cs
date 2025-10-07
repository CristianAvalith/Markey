using AutoMapper;
using Markey.CrossCutting.Excepciones;
using Markey.Domain.Interfaces;
using Markey.Domain.Models;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.DTOs;
using Markey.Persistance.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json;
namespace Markey.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;


    public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ICatalogRepository catalogRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _catalogRepository = catalogRepository;
    }

    #region REGISTER METHODS
    public async Task<Guid> RegisterAsync(UserData userData)
    {

        await _catalogRepository.GetOccupationById(userData.OccupationId);

        var existingUser = await _userManager.FindByEmailAsync(userData.Email);

        if (existingUser != null)
        {
            throw new RegisterException($"El usuario con el correo '{userData.Email}' ya existe.");
        }

        var user = _mapper.Map<User>(userData);
        user.Id = Guid.NewGuid();
        user.LockoutEnabled = false;
        user.CreatedAt = DateTime.UtcNow;

        if (userData.Photo != null && userData.Photo.Length > 0)
        {
            user.PhotoUrl = await SaveUserPhotoAsync(userData.Photo);
        }

        var result = await _userManager.CreateAsync(user, userData.Password);

        if (!result.Succeeded)
        {
            throw new RegisterException(result.Errors);
        }

        return user.Id;
    }

    private async Task<string> SaveUserPhotoAsync(IFormFile photo)
    {
        var photosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos");

        if (!Directory.Exists(photosPath))
        {
            Directory.CreateDirectory(photosPath);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
        var filePath = Path.Combine(photosPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        var baseUrl = _configuration["AppSettings:BaseUrl"];
        var photoUrl = $"{baseUrl}/photos/{fileName}";

        return photoUrl;
    }


    #endregion

    #region GET METHODS
    public async Task<GetUserData> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        var userData = _mapper.Map<GetUserData>(user);
        return userData;
    }

    public async Task<List<ResumeUserData>> ListUsersByFilterAsync(string fullName, int pageSize, int pageNumber)
    {
        var users = await _userRepository.ListUsersByFilterAsync(fullName, pageSize, pageNumber);
        return _mapper.Map<List<ResumeUserData>>(users);
    }
    #endregion

    #region UPDATE METHODS
    public async Task<GetUserData> PatchUserAsync(UserDataToUpdate userToUpdate)
    {
        var user = await _userRepository.UpdateAsync(_mapper.Map<User>(userToUpdate));
        var userData = _mapper.Map<GetUserData>(user);
        return userData;
    }

    public async Task<string> UpdateUserPhotoAsync(Guid userId, IFormFile photo)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (!string.IsNullOrEmpty(user.PhotoUrl))
        {
            await DeleteUserPhotoAsync(user.PhotoUrl);
        }

        var newPhotoUrl = await SaveUserPhotoAsync(photo);

        user.PhotoUrl = newPhotoUrl;
        await _userManager.UpdateAsync(user);

        return newPhotoUrl;
    }

    private async Task DeleteUserPhotoAsync(string photoUrl)
    {
        try
        {
            var fileName = Path.GetFileName(new Uri(photoUrl).LocalPath);
            var photosPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos");
            var filePath = Path.Combine(photosPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception)
        {
        }

        await Task.CompletedTask;
    }
    #endregion
}