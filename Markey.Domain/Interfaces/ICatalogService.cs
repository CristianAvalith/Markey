using Markey.Persistance.DTOs;

namespace Markey.Domain.Interfaces;

public interface ICatalogService
{
    Task<List<OccupationData>> GetOccupationsAsync();
}