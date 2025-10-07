using Markey.Persistance.DTOs;

namespace Markey.Application.Catalog.GetOccupations;

public class GetOccupationsResponse
{
    public List<OccupationData> Occupations { get; set; }
}