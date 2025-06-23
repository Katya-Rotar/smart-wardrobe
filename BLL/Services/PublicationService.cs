using AutoMapper;
using BLL.DTO.Publication;
using BLL.DTO.Tag;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Helpers;
using DAL.Helpers.Params;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class PublicationService : IPublicationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITagService _tagService;
    

    public PublicationService(IUnitOfWork unitOfWork, IMapper mapper, ITagService tagService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tagService = tagService;
    }

    public PagedList<PublicationListDto> GetAllPublication(PublicationParams parameters)
    {
        var publication = _unitOfWork.Publications.GetAllPublication(parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publication);
    }

    public async Task<PublicationDetailsDto?> GetPublicationDetailsAsync(int id)
    {
        var publication = await _unitOfWork.Publications.GetPublicationDetailsAsync(id);
        return _mapper.Map<PublicationDetailsDto?>(publication);
    }

    public async Task<PagedList<PublicationListDto>> GetByUserAsync(int userId, PublicationParams parameters)
    {
        var publication = await _unitOfWork.Publications.GetByUserAsync(userId, parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publication);
    }

    public async Task<int> CreatePublicationAsync(PublicationDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = _mapper.Map<Publication>(dto);

            await _unitOfWork.Publications.AddAsync(publication);
            await _unitOfWork.SaveAsync();
            
            var publicationTags = new List<PublicationTag>();
            foreach (var tagName in dto.Tags.Distinct())
            {
                var tagId = await _tagService.AddTagAsync(new CreateTagDto { TagName = tagName }, cancellationToken);

                publicationTags.Add(new PublicationTag
                {
                    PublicationID = publication.Id,
                    TagID = tagId
                });
            }
            
            await _unitOfWork.PublicationTags.AddRangeAsync(publicationTags);
            
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return publication.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdatePublicationAsync(int id, UpdatePublicationDto publicationDto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = await _unitOfWork.Publications.GetByIdAsync(id);
            if (publication == null) throw new KeyNotFoundException("Publication not found.");

            _mapper.Map(publicationDto, publication);
            await _unitOfWork.Publications.UpdateAsync(publication);
            
            var existingTags = await _unitOfWork.PublicationTags
                .GetTagsByPublicationIdAsync(publication.Id);
            var existingTagNames = existingTags.Select(t => t.Tag.TagName).ToList();
            
            var newTagNames = publicationDto.Tags.Distinct().ToList();
            
            var tagsToAdd = newTagNames.Except(existingTagNames).ToList();
            var tagsToRemove = existingTagNames.Except(newTagNames).ToList();
            
            foreach (var tagName in tagsToAdd)
            {
                var tagId = await _tagService.AddTagAsync(new CreateTagDto { TagName = tagName }, cancellationToken);

                await _unitOfWork.PublicationTags.AddAsync(new PublicationTag
                {
                    PublicationID = publication.Id,
                    TagID = tagId
                });
            }
            
            foreach (var tagName in tagsToRemove)
            {
                var tag = await _unitOfWork.Tags.GetByNameAsync(tagName);
                if (tag != null)
                {
                    await _unitOfWork.PublicationTags.DeleteByPublicationAndTagAsync(publication.Id, tag.Id);
                }
            }

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }


    public async Task DeletePublicationAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = await _unitOfWork.Publications.GetByIdAsync(id);
            if (publication == null) throw new KeyNotFoundException("publication not found.");

            await _unitOfWork.Publications.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
    
    public async Task<PagedList<PublicationListDto>> GetFollowingsPublicationsAsync(int userId, PublicationParams parameters)
    {
        var publications = await _unitOfWork.Publications.GetPublicationsOfFollowingsAsync(userId, parameters);
        return _mapper.Map<PagedList<PublicationListDto>>(publications);
    }
}