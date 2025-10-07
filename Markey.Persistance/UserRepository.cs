using Microsoft.EntityFrameworkCore;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.Interface;
using Markey.CrossCutting.Excepciones;
namespace Markey.Persistance;

#pragma warning disable CA1862
public class UserRepository : IUserRepository
{
    private readonly MyDbContext _dbcontext;

    public UserRepository(MyDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    #region GET METHODS
    public async Task<User> GetUserByUserNameAsync(string userName)
    {
        return await _dbcontext.Set<User>().Where(u => u.UserName == userName).SingleOrDefaultAsync() ?? throw new UserNotFoundException();
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        return await _dbcontext.Set<User>().Where(u => u.Id == userId).Include(u => u.Occupation).FirstOrDefaultAsync() ?? throw new UserNotFoundException();
    }

    #endregion

    #region LIST METHODS

    public async Task<List<User>> ListUsersByFilterAsync(string fullName, int pageSize, int pageNumber)
    {
        IQueryable<User> query = _dbcontext.Set<User>();

        query = FilterByFullName(fullName, query);
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        query = query.Include(u => u.Occupation);

        return await query.ToListAsync();

    }

    private static IQueryable<User> FilterByFullName(string fullName, IQueryable<User> query)
    {
        if (!string.IsNullOrWhiteSpace(fullName))
        {
            query = query.Where(u => u.FullName.ToLower().Contains(fullName));
        }

        return query;
    }

    #endregion

    #region UPDATE METHODS
    public async Task<User> UpdateAsync(User userToUpdate)
    {

        var occupation = await _dbcontext.Set<Occupation>().Where(o => o.Id == userToUpdate.OccupationId).FirstOrDefaultAsync() ?? throw new OcupationNotFoundException();

        var user = await _dbcontext.Set<User>().Where(u => u.Id == userToUpdate.Id).Include(u => u.Occupation).SingleOrDefaultAsync() ?? throw new UserNotFoundException();
        user.FullName = userToUpdate.FullName;
        user.PhoneNumber = userToUpdate.PhoneNumber;
        user.OccupationId = userToUpdate.OccupationId; 

        await _dbcontext.SaveChangesAsync();
        return user;
    }

    #endregion
}
