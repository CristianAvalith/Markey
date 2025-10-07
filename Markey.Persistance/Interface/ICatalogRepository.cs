using Markey.Persistance.Data.Tables;
using Markey.Persistance.DTOs;

namespace Markey.Persistance.Interface;

public interface ICatalogRepository
{
    Task<List<OccupationData>> GetOccupationsAsync();
    Task<Occupation> GetOccupationById(Guid id);
}