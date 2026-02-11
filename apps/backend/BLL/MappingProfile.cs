using BLL.DTO;
using BLL.DTO.Category;
using BLL.DTO.Outfit;
using BLL.DTO.OutfitGroup;
using BLL.DTO.Season;
using BLL.DTO.Style;
using BLL.DTO.Tag;
using BLL.DTO.TemperatureSuitability;
using BLL.DTO.Type;
using BLL.DTO.User;
using BLL.Helpers.Mapping;
using DAL.Entities;
using DAL.Helpers;
using Profile = AutoMapper.Profile;
using Type = DAL.Entities.Type;

namespace BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
        
        CreateMap<ClothingItem, ClothingItemDto>().ReverseMap();
        CreateMap<CreateClothingItemDto, ClothingItem>().ReverseMap();
        CreateMap<ClothingItem, ClothingItemAllDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Type.TypeName))
            .ForMember(dest => dest.TemperatureSuitabilityName, opt => opt.MapFrom(src => src.TemperatureSuitability.TemperatureSuitabilityName))
            .ForMember(dest => dest.StyleNames, opt => opt.MapFrom(src => src.Styles.Select(s => s.Style.StyleName)))
            .ForMember(dest => dest.SeasonNames, opt => opt.MapFrom(src => src.Seasons.Select(s => s.Season.SeasonName)));
        
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Type, TypeDto>().ReverseMap();
        CreateMap<TemperatureSuitability, TemperatureSuitabilityDto>().ReverseMap();
        CreateMap<Style, StyleDto>().ReverseMap();
        CreateMap<Season, SeasonDto>().ReverseMap();
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<Tag, CreateTagDto>().ReverseMap();
        
        CreateMap<ClothingItem, OutfitItemsDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Outfit, OutfitDto>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(s => s.Tag.TagName)))
            .ForMember(dest => dest.GroupNames, opt => opt.MapFrom(src => src.GroupItems.Select(s => s.OutfitGroup.GroupName)))
            .ForMember(dest => dest.ItemNames, opt => opt.MapFrom(src => src.Items.Select(i => i.ClothingItem)))
            .ForMember(dest => dest.TemperatureSuitabilityName, opt => opt.MapFrom(src => src.TemperatureSuitability.TemperatureSuitabilityName))
            .ForMember(dest => dest.StyleNames, opt => opt.MapFrom(src => src.Styles.Select(s => s.Style.StyleName)))
            .ForMember(dest => dest.SeasonNames, opt => opt.MapFrom(src => src.Seasons.Select(s => s.Season.SeasonName)));

        CreateMap<Outfit, UpdateOutfitDto>()
            .ForMember(dest => dest.TagIDs, opt => opt.MapFrom(src => src.Tags.Select(t => t.Id).ToList()))
            .ForMember(dest => dest.StyleIDs, opt => opt.MapFrom(src => src.Styles.Select(s => s.Id).ToList()))
            .ForMember(dest => dest.SeasonIDs, opt => opt.MapFrom(src => src.Seasons.Select(se => se.Id).ToList()))
            .ForMember(dest => dest.ClothingItemIDs, opt => opt.MapFrom(src => src.GroupItems.Select(gi => gi.Id).ToList()));
        
        CreateMap<OutfitGroup, OutfitGroupDto>().ReverseMap();
        CreateMap<OutfitGroup, CreateOutfitGroupDto>().ReverseMap();
        
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>()
            .ForMember(dto => dto.Role, opt => opt.Ignore());
    }
}