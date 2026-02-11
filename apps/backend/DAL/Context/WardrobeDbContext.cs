using DAL.Context.Configuration;
using DAL.Seeding;

namespace DAL.Context;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;


public class WardrobeDbContext : DbContext
{
    public WardrobeDbContext(DbContextOptions<WardrobeDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TemperatureSuitability> TemperatureSuitabilities { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<TypeCategory> TypeCategories { get; set; }
    public DbSet<ClothingItem> ClothingItems { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<ClothingItemSeason> ClothingItemSeasons { get; set; }
    public DbSet<Style> Styles { get; set; }
    public DbSet<ClothingItemStyle> ClothingItemStyles { get; set; }
    public DbSet<Outfit> Outfits { get; set; }
    public DbSet<OutfitItem> OutfitItems { get; set; }
    public DbSet<OutfitStyle> OutfitStyles { get; set; }
    public DbSet<OutfitSeason> OutfitSeasons { get; set; }
    public DbSet<OutfitGroup> OutfitGroups { get; set; }
    public DbSet<OutfitGroupItem> OutfitGroupItems { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<OutfitTag> OutfitTags { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Publication> Publications { get; set; }
    public DbSet<PublicationTag> PublicationTags { get; set; }
    public DbSet<PostLike> PostLikes { get; set; }
    public DbSet<SavedPost> SavedPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentLike> CommentLikes { get; set; }
    public DbSet<Follower> Followers { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new TemperatureSuitabilityConfiguration());
        modelBuilder.ApplyConfiguration(new TypeConfiguration());
        modelBuilder.ApplyConfiguration(new TypeCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ClothingItemConfiguration());
        modelBuilder.ApplyConfiguration(new SeasonConfiguration());
        modelBuilder.ApplyConfiguration(new ClothingItemSeasonConfiguration());
        modelBuilder.ApplyConfiguration(new StyleConfiguration());
        modelBuilder.ApplyConfiguration(new ClothingItemStyleConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitItemConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitStyleConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitSeasonConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitGroupConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitGroupItemConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new OutfitTagConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new PublicationConfiguration());
        modelBuilder.ApplyConfiguration(new PublicationTagConfiguration());
        modelBuilder.ApplyConfiguration(new PostLikeConfiguration());
        modelBuilder.ApplyConfiguration(new SavedPostConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeConfiguration());
        modelBuilder.ApplyConfiguration(new FollowerConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());

        modelBuilder.Entity<Category>().HasData(DataSeeder.GenerateCategories());
        modelBuilder.Entity<ClothingItem>().HasData(DataSeeder.GenerateClothingItems());
        modelBuilder.Entity<ClothingItemSeason>().HasData(DataSeeder.GenerateClothingItemSeasons());
        modelBuilder.Entity<ClothingItemStyle>().HasData(DataSeeder.GenerateClothingItemStyles());
        modelBuilder.Entity<Comment>().HasData(DataSeeder.GenerateComments());
        modelBuilder.Entity<CommentLike>().HasData(DataSeeder.GenerateCommentLikes());
        modelBuilder.Entity<Event>().HasData(DataSeeder.GenerateEvents());
        modelBuilder.Entity<Follower>().HasData(DataSeeder.GenerateFollowers());
        modelBuilder.Entity<Notification>().HasData(DataSeeder.GenerateNotifications());
        modelBuilder.Entity<Outfit>().HasData(DataSeeder.GenerateOutfits());
        modelBuilder.Entity<OutfitGroup>().HasData(DataSeeder.GenerateOutfitGroups());
        modelBuilder.Entity<OutfitGroupItem>().HasData(DataSeeder.GenerateOutfitGroupItems());
        modelBuilder.Entity<OutfitItem>().HasData(DataSeeder.GenerateOutfitItems());
        modelBuilder.Entity<OutfitSeason>().HasData(DataSeeder.GenerateOutfitSeasons());
        modelBuilder.Entity<OutfitStyle>().HasData(DataSeeder.GenerateOutfitStyles());
        modelBuilder.Entity<OutfitTag>().HasData(DataSeeder.GenerateOutfitTags());
        modelBuilder.Entity<PostLike>().HasData(DataSeeder.GeneratePostLikes());
        modelBuilder.Entity<Profile>().HasData(DataSeeder.GenerateProfiles());
        modelBuilder.Entity<Publication>().HasData(DataSeeder.GeneratePublications());
        modelBuilder.Entity<PublicationTag>().HasData(DataSeeder.GeneratePublicationTags());
        modelBuilder.Entity<SavedPost>().HasData(DataSeeder.GenerateSavedPosts());
        modelBuilder.Entity<Season>().HasData(DataSeeder.GenerateSeasons());
        modelBuilder.Entity<Style>().HasData(DataSeeder.GenerateStyles());
        modelBuilder.Entity<Tag>().HasData(DataSeeder.GenerateTags());
        modelBuilder.Entity<TemperatureSuitability>().HasData(DataSeeder.GenerateTemperatureSuitabilities());
        modelBuilder.Entity<Type>().HasData(DataSeeder.GenerateTypes());
        modelBuilder.Entity<TypeCategory>().HasData(DataSeeder.GenerateTypeCategories());
        modelBuilder.Entity<User>().HasData(DataSeeder.GenerateUsers());
    }
}
