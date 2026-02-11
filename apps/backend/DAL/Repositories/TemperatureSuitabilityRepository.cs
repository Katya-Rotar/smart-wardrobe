using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

public class TemperatureSuitabilityRepository : GenericRepository<TemperatureSuitability>, ITemperatureSuitabilityRepository
{
    public TemperatureSuitabilityRepository(WardrobeDbContext context) : base(context)
    {
    }
}