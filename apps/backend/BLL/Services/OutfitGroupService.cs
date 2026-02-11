using AutoMapper;
using BLL.DTO.OutfitGroup;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class OutfitGroupService : IOutfitGroupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OutfitGroupService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OutfitGroupDto>> GetByUserIdAsync(int userId)
    {
        var groups = await _unitOfWork.OutfitGroups.GetAllByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<OutfitGroupDto>>(groups);
    }

    public async Task<OutfitGroupDto?> GetByIdAsync(int id)
    {
        var group = await _unitOfWork.OutfitGroups.GetByIdAsync(id);
        return group != null ? _mapper.Map<OutfitGroupDto>(group) : null;
    }

    public async Task CreateAsync(CreateOutfitGroupDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var group = new OutfitGroup
            {
                UserID = dto.UserID,
                GroupName = dto.GroupName,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.OutfitGroups.AddAsync(group);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task UpdateAsync(OutfitGroupDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var group = await _unitOfWork.OutfitGroups.GetByIdAsync(dto.Id);
            if (group == null)
                throw new KeyNotFoundException("Group not found");

            group.GroupName = dto.GroupName;
            group.Description = dto.Description;

            _unitOfWork.OutfitGroups.UpdateAsync(group);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var group = await _unitOfWork.OutfitGroups.GetByIdAsync(id);
            if (group == null)
                throw new KeyNotFoundException("Group not found");

            await _unitOfWork.OutfitGroups.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
