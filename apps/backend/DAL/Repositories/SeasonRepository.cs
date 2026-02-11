using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

public class SeasonRepository : GenericRepository<Season>, ISeasonRepository
{
    public SeasonRepository(WardrobeDbContext context) : base(context)
    {
    }
}