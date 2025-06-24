namespace DAL.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IClothingItemRepository ClothingItems { get; }
    ICategoryRepository Categories { get; }
    ITypeRepository Types { get; }
    ITemperatureSuitabilityRepository TemperatureSuitability { get; }
    IStyleRepository Styles { get; }
    ISeasonRepository Seasons { get; }
    ITagRepository Tags { get; }
    IOutfitRepository Outfits { get; }
    IOutfitGroupRepository OutfitGroups { get; }
    IUserRepository Users { get; }
    IPublicationRepository Publications { get; }
    IPublicationTagRepository PublicationTags { get; }
    IFollowerRepository Followers { get; }
    ISavedPostRepository SavedPosts { get; }
    IPostLikeRepository PostLikes { get; }
    Task SaveAsync();
    Task BeginTransactionAsync(CancellationToken cancellationToken);
    Task CommitTransactionAsync(CancellationToken cancellationToken);
    Task RollbackTransactionAsync(CancellationToken cancellationToken);
}