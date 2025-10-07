using Markey.Persistance.Data.Tables;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Markey.Persistance.Interface;

public interface IUserRepository
{
    Task<User> GetUserByUserNameAsync(string userName);
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> UpdateAsync(User userToUpdate);
    Task<List<User>> ListUsersByFilterAsync(string fullName, int pageSize, int pageNumber);
}
