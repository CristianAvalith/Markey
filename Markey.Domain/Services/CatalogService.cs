using Markey.Domain.Interfaces;
using Markey.Persistance.DTOs;
using Markey.Persistance.Interface;

namespace Markey.Domain.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task<List<OccupationData>> GetOccupationsAsync()
    {
        return await _catalogRepository.GetOccupationsAsync();
    }
}