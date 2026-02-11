using DAL.Entities;
using Type = DAL.Entities.Type;

namespace DAL.Seeding;

public abstract class DataSeeder
{
    public static IReadOnlyCollection<Category> GenerateCategories()
    {
        return
        [
            new Category { Id = 1, CategoryName = "Outerwear"},
            new Category { Id = 2, CategoryName = "Tops"},
            new Category { Id = 3, CategoryName = "Bottoms"},
            new Category { Id = 4, CategoryName = "Dresses"},
            new Category { Id = 5, CategoryName = "Shoes"},
            new Category { Id = 6, CategoryName = "Accessories"}
        ];
    }
    
    public static IReadOnlyCollection<ClothingItem> GenerateClothingItems()
    {
        return
        [
            new ClothingItem
            {
                Id = 1,
                UserID = 1,
                Name = "Blue Denim Jacket",
                CategoryID = 1, // Outerwear
                TypeID = 1,     // e.g., Jacket
                Color = "Blue",
                TemperatureSuitabilityID = 4,
                ImageURL = "https://e7.pngegg.com/pngimages/324/140/png-clipart-blue-washed-denim-button-up-jacket-jean-jacket-denim-h-m-jeans-women-s-jackets-blue-women-accessories-thumbnail.png",
                LastWornDate = new DateTime(2025, 5, 6, 0, 0, 0, DateTimeKind.Utc)

            },
            new ClothingItem
            {
                Id = 2,
                UserID = 1,
                Name = "White T-Shirt",
                CategoryID = 2, // Tops
                TypeID = 3,     // T-Shirt
                Color = "White",
                TemperatureSuitabilityID = 5,
                ImageURL = "https://png.pngtree.com/png-clipart/20230930/original/pngtree-white-t-shirt-mockup-realistic-t-shirt-png-image_13020297.png",
                LastWornDate = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new ClothingItem
            {
                Id = 3,
                UserID = 1,
                Name = "Black Jeans",
                CategoryID = 3, // Bottoms
                TypeID = 6,     // Jeans
                Color = "Black",
                TemperatureSuitabilityID = 4,
                ImageURL = "https://www.pngpacks.com/uploads/data/2058/IMG_bbgqbicsmHvp.png",
                LastWornDate = new DateTime(2025, 5, 11, 0, 0, 0, DateTimeKind.Utc)
            },
            new ClothingItem
            {
                Id = 4,
                UserID = 1,
                Name = "Summer Dress",
                CategoryID = 4, // Dresses
                TypeID = 11,     // Dress
                Color = "Yellow",
                TemperatureSuitabilityID = 5,
                ImageURL = "https://e7.pngegg.com/pngimages/826/743/png-clipart-cocktail-dress-skirt-gown-dirndl-summer-clothes-fashion-party-dress-thumbnail.png",
                LastWornDate = null
            }
        ];
    }
    
    public static IReadOnlyCollection<ClothingItemSeason> GenerateClothingItemSeasons()
    {
        return
        [
            new ClothingItemSeason
            {
                Id = 1,
                ClothingItemID = 1, // Blue Denim Jacket
                SeasonID = 4       // Winter
            },
            new ClothingItemSeason
            {
                Id = 2,
                ClothingItemID = 2, // White T-Shirt
                SeasonID = 2       // Summer
            },
            new ClothingItemSeason
            {
                Id = 3,
                ClothingItemID = 3, // Black Jeans
                SeasonID = 1       // Spring
            },
            new ClothingItemSeason
            {
                Id = 4,
                ClothingItemID = 4, // Summer Dress
                SeasonID = 2       // Summer
            }
        ];
    }

    public static IReadOnlyCollection<ClothingItemStyle> GenerateClothingItemStyles()
    {
        return
        [
            new ClothingItemStyle
            {
                Id = 1,
                ClothingItemID = 1, // Blue Denim Jacket
                StyleID = 1         // Casual
            },
            new ClothingItemStyle
            {
                Id = 2,
                ClothingItemID = 2, // White T-Shirt
                StyleID = 3         // Sporty
            },
            new ClothingItemStyle
            {
                Id = 3,
                ClothingItemID = 3, // Black Jeans
                StyleID = 2         // Formal
            },
            new ClothingItemStyle
            {
                Id = 4,
                ClothingItemID = 4, // Summer Dress
                StyleID = 1         // Casual
            }
        ];
    }

    public static IReadOnlyCollection<Comment> GenerateComments()
    {
        return
        [
            new Comment
            {
                Id = 1,
                ProfileID = 1,         // Profile 1
                PublicationID = 1,     // Publication 1
                Content = "Great outfit!",
                CreatedAt = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc)
            },
            new Comment
            {
                Id = 2,
                ProfileID = 2,         // Profile 2
                PublicationID = 1,     // Publication 1
                Content = "Where did you get those shoes?",
                CreatedAt = new DateTime(2025, 5, 7, 0, 0, 0, DateTimeKind.Utc)
            }
        ];
    }
    
    public static IReadOnlyCollection<CommentLike> GenerateCommentLikes()
    {
        return
        [
            new CommentLike
            {
                Id = 1,
                ProfileID = 2,
                CommentID = 1
            },
            new CommentLike
            {
                Id = 3,
                ProfileID = 1,
                CommentID = 2
            }
        ];
    }

    public static IReadOnlyCollection<Event> GenerateEvents()
    {
        return
        [
            new Event
            {
                Id = 1,
                UserID = 1,
                Name = "Friend's Wedding",
                Date = new DateTime(2025, 6, 20, 0, 0, 0, DateTimeKind.Utc),
                Location = "Lviv",
                DressCode = "Formal",
                OutfitID = 1
            },
            new Event
            {
                Id = 2,
                UserID = 1,
                Name = "Office Party",
                Date = new DateTime(2025, 7, 5, 0, 0, 0, DateTimeKind.Utc),
                Location = "Kyiv",
                DressCode = "Business Casual",
                OutfitID = 2
            },
            new Event
            {
                Id = 3,
                UserID = 1,
                Name = "Birthday Picnic",
                Date = new DateTime(2025, 8, 1, 0, 0, 0, DateTimeKind.Utc),
                Location = "Park",
                DressCode = "Casual",
                OutfitID = null
            }
        ];
    }

    public static IReadOnlyCollection<Follower> GenerateFollowers()
    {
        return
        [
            new Follower
            {
                Id = 1,
                FollowerID = 1,
                FollowingID = 2,
                CreatedAt = new DateTime(2025, 4, 30, 0, 0, 0, DateTimeKind.Utc)
            },
            new Follower
            {
                Id = 2,
                FollowerID = 2,
                FollowingID = 1,
                CreatedAt = new DateTime(2025, 5, 3, 0, 0, 0, DateTimeKind.Utc)
            }
        ];
    }

    public static IReadOnlyCollection<Notification> GenerateNotifications()
    {
        return
        [
            new Notification
            {
                Id = 1,
                UserID = 1,
                IsRead = false,
                CreatedAt = new DateTime(2025, 5, 8, 0, 0, 0, DateTimeKind.Utc),
                EventID = 1
            },
            new Notification
            {
                Id = 2,
                UserID = 1,
                IsRead = true,
                CreatedAt = new DateTime(2025, 5, 5, 0, 0, 0, DateTimeKind.Utc),
                EventID = 2
            }
        ];
    }
    
    public static IReadOnlyCollection<Outfit> GenerateOutfits()
    {
        return 
        [
            new Outfit { Id = 1, UserID = 1, TemperatureSuitabilityID = 1},
            new Outfit { Id = 2, UserID = 1, TemperatureSuitabilityID = 2}
        ];
    }

    public static IReadOnlyCollection<OutfitGroup> GenerateOutfitGroups()
    {
        return
        [
            new OutfitGroup { Id = 1, UserID = 1, GroupName = "Casual Summer", CreatedAt = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc) },
            new OutfitGroup { Id = 2, UserID = 1, GroupName = "Formal Evening", CreatedAt = new DateTime(2025, 5, 10, 0, 0, 0, DateTimeKind.Utc) }
        ];
    }

    public static IReadOnlyCollection<OutfitGroupItem> GenerateOutfitGroupItems()
    {
        return
        [
            new OutfitGroupItem {Id = 1, OutfitGroupID = 1, OutfitID = 1}
        ];
    }

    public static IReadOnlyCollection<OutfitItem> GenerateOutfitItems()
    {
        return
        [
            new OutfitItem
            {
                Id = 1,
                OutfitID = 1,
                ClothingItemID = 2
            },
            new OutfitItem
            {
                Id = 2,
                OutfitID = 1,
                ClothingItemID = 3
            },
            new OutfitItem
            {
                Id = 3,
                OutfitID = 2,
                ClothingItemID = 4
            }
        ];
    }

    public static IReadOnlyCollection<OutfitSeason> GenerateOutfitSeasons()
    {
        return
        [
            new OutfitSeason { Id = 1, OutfitID = 1, SeasonID = 2 },
            new OutfitSeason { Id = 2, OutfitID = 2, SeasonID = 2 }
        ];
    }

    public static IReadOnlyCollection<OutfitStyle> GenerateOutfitStyles()
    {
        return [
            new OutfitStyle { Id = 1, OutfitID = 1, StyleID = 2 },
            new OutfitStyle { Id = 2, OutfitID = 2, StyleID = 1 }
        ];
    }

    public static IReadOnlyCollection<OutfitTag> GenerateOutfitTags()
    {
        return [
            new OutfitTag { Id = 1, OutfitID = 1, TagID = 1 },
            new OutfitTag { Id = 2, OutfitID = 1, TagID = 3 },
            new OutfitTag { Id = 3, OutfitID = 2, TagID = 3 }
        ];
    }

    public static IReadOnlyCollection<PostLike> GeneratePostLikes()
    {
        return [
            new PostLike {Id = 1, ProfileID = 1, PublicationID = 1},
            new PostLike {Id = 2, ProfileID = 2, PublicationID = 1}
        ];
    }

    public static IReadOnlyCollection<Profile> GenerateProfiles()
    {
        return [
            new Profile
            {
                Id = 1,
                UserID = 1,
                Username = "fashionlover123",
                ProfileImage = "https://i.pinimg.com/564x/39/33/f6/3933f64de1724bb67264818810e3f2cb.jpg",
                Bio = "Lover of vintage style and cozy sweaters."
            },
            new Profile
            {
                Id = 2,
                UserID = 2,
                Username = "Anna",
                ProfileImage = "https://cdn.expertphotography.com/wp-content/uploads/2020/08/social-media-profile-photos.jpg",
                Bio = "Streetwear is my passion."
            }
        ];
    }

    public static IReadOnlyCollection<Publication> GeneratePublications()
    {
        return [
            new Publication
            {
                Id = 1,
                ProfileID = 1,
                OutfitID = 1,
                ImageURL = "https://fashionjackson.com/wp-content/uploads/2017/06/Fashion-Jackson-Everlane-White-Tshirt-Zara-Ripped-Black-Skinny-Jeans.jpg",
                CommentingOptions = true
            }
        ];
    }

    public static IReadOnlyCollection<PublicationTag> GeneratePublicationTags()
    {
        return [
            new PublicationTag { Id = 1, PublicationID = 1, TagID = 2 },
            new PublicationTag { Id = 2, PublicationID = 1, TagID = 3 }
        ];
    }

    public static IReadOnlyCollection<SavedPost> GenerateSavedPosts()
    {
        return [
            new SavedPost { Id = 1, ProfileID = 1, PublicationID = 1}
        ];
    }

    public static IReadOnlyCollection<Style> GenerateStyles()
    {
        return
        [
            new Style { Id = 1, StyleName = "Casual" },
            new Style { Id = 2, StyleName = "Formal" },
            new Style { Id = 3, StyleName = "Sporty" },
            new Style { Id = 4, StyleName = "Party" },
            new Style { Id = 5, StyleName = "Vintage" }
        ];
    }

    public static IReadOnlyCollection<Season> GenerateSeasons()
    {
        return
        [
            new Season { Id = 1, SeasonName = "Spring" },
            new Season { Id = 2, SeasonName = "Summer" },
            new Season { Id = 3, SeasonName = "Autumn" },
            new Season { Id = 4, SeasonName = "Winter" }
        ];
    }

    public static IReadOnlyCollection<Tag> GenerateTags()
    {
        return
        [
            new Tag { Id = 1, TagName = "Casual" },
            new Tag { Id = 2, TagName = "Formal" },
            new Tag { Id = 3, TagName = "Summer" },
            new Tag { Id = 4, TagName = "Winter" },
            new Tag { Id = 5, TagName = "Athletic" }
        ];
    }

    public static IReadOnlyCollection<Type> GenerateTypes()
    {
        return
        [
            new Type { Id = 1, TypeName = "Jacket" },
            new Type { Id = 2, TypeName = "Coat" },
            new Type { Id = 3, TypeName = "T-Shirt" },
            new Type { Id = 4, TypeName = "Blouse" },
            new Type { Id = 5, TypeName = "Sweatshirt" },
            new Type { Id = 6, TypeName = "Jeans" },
            new Type { Id = 7, TypeName = "Trousers" },
            new Type { Id = 8, TypeName = "Shorts" },
            new Type { Id = 9, TypeName = "Evening Dress" },
            new Type { Id = 10, TypeName = "Casual Dress" },
            new Type { Id = 11, TypeName = "Sundress" },
            new Type { Id = 12, TypeName = "Sneakers" },
            new Type { Id = 13, TypeName = "Boots" },
            new Type { Id = 14, TypeName = "Heels" }
        ];
    }

    public static IReadOnlyCollection<TypeCategory> GenerateTypeCategories()
    {
        return 
        [
            new TypeCategory { Id = 1, CategoryID = 1, TypeID = 1},
            new TypeCategory { Id = 2, CategoryID = 1, TypeID = 2},
            new TypeCategory { Id = 3, CategoryID = 2, TypeID = 3},
            new TypeCategory { Id = 4, CategoryID = 2, TypeID = 4},
            new TypeCategory { Id = 5, CategoryID = 2, TypeID = 5},
            new TypeCategory { Id = 6, CategoryID = 3, TypeID = 6},
            new TypeCategory { Id = 7, CategoryID = 3, TypeID = 7},
            new TypeCategory { Id = 8, CategoryID = 3, TypeID = 8},
            new TypeCategory { Id = 9, CategoryID = 4, TypeID = 9},
            new TypeCategory { Id = 10, CategoryID = 4, TypeID = 10},
            new TypeCategory { Id = 11, CategoryID = 4, TypeID = 11},
            new TypeCategory { Id = 12, CategoryID = 5, TypeID = 12},
            new TypeCategory { Id = 13, CategoryID = 5, TypeID = 13},
            new TypeCategory { Id = 14, CategoryID = 5, TypeID = 14},
        ];
    }

    public static IReadOnlyCollection<TemperatureSuitability> GenerateTemperatureSuitabilities()
    {
        return [
            new TemperatureSuitability {Id = 1, TemperatureSuitabilityName = "Extra could (-30 to -20)"},
            new TemperatureSuitability {Id = 2, TemperatureSuitabilityName = "Very cold (-20 to -10)"},
            new TemperatureSuitability {Id = 3, TemperatureSuitabilityName = "Cold (-10 to 0)"},
            new TemperatureSuitability {Id = 4, TemperatureSuitabilityName = "Chilly (0 to +10)"},
            new TemperatureSuitability {Id = 5, TemperatureSuitabilityName = "Warm (+10 to +20)"},
            new TemperatureSuitability {Id = 6, TemperatureSuitabilityName = "Hot (+20 to +30)"},
        ];
    }
    
    public static IReadOnlyCollection<User> GenerateUsers()
    {
        return 
        [
            new User
            {
                Id = 1, 
                Username = "Katya", 
                ProfileImage = "https://i.pinimg.com/564x/39/33/f6/3933f64de1724bb67264818810e3f2cb.jpg",
                PasswordHash = "katyaPassword",
                Email = "katya@gmail.com",
                Role = "Premium"
            },
            new User
            {
                Id = 2, 
                Username = "Anna", 
                ProfileImage = "https://cdn.expertphotography.com/wp-content/uploads/2020/08/social-media-profile-photos.jpg",
                PasswordHash = "AnnaPassword",
                Email = "anna@gmail.com",
                Role = "User"
            },
            new User
            {
                Id = 3, 
                Username = "Admin", 
                ProfileImage = null,
                PasswordHash = "Admin",
                Email = "Admin@gmail.com",
                Role = "Admin"
            }
        ];
    }
}