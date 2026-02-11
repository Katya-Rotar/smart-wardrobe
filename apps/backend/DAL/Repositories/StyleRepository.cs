using DAL.Context;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories;

public class StyleRepository : GenericRepository<Style>, IStyleRepository
{
    public StyleRepository(WardrobeDbContext context) : base(context)
    {
    }
}