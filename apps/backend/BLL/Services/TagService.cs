using AutoMapper;
using BLL.DTO.Tag;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class TagService : ITagService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TagService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagDto>> GetAll()
    {
        var tag = await _unitOfWork.Tags.GetAllAsync();
        return _mapper.Map<IEnumerable<TagDto>>(tag);
    }

    public async Task<TagDto> GetTagByIdAsync(int id)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(id);
        return _mapper.Map<TagDto>(tag);
    }

    public async Task<int> AddTagAsync(CreateTagDto tagDto, CancellationToken cancellationToken)
    {
        var existingTag = await _unitOfWork.Tags.GetByNameAsync(tagDto.TagName);
        if (existingTag != null)
        {
            return existingTag.Id;
        }

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var tag = _mapper.Map<Tag>(tagDto);

            await _unitOfWork.Tags.AddAsync(tag);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return tag.Id;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}