using Markey.CrossCutting.Excepciones;
using Markey.Persistance.Data.Tables;
using Markey.Persistance.DTOs;
using Markey.Persistance.Interface;
using Microsoft.EntityFrameworkCore;

namespace Markey.Persistance;

public class CatalogRepository : ICatalogRepository
{
    private readonly MyDbContext _dbContext;

    public CatalogRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<OccupationData>> GetOccupationsAsync()
    {
        var occupation = await _dbContext.Set<Occupation>().ToListAsync();

        return occupation.Select(o => new OccupationData
        {
            Id = o.Id,
            Name = o.Name
        }).ToList();
    }

    public async Task<Occupation> GetOccupationById(Guid id) {

        return await _dbContext.Set<Occupation>().Where(o => o.Id == id).FirstOrDefaultAsync() ?? throw new OcupationNotFoundException();

    }


}