using AutoMapper;
using BLL.DTO.SavedPost;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class SavedPostService : ISavedPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SavedPostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> SavePostAsync(SavedPostDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var saved = await IsPostSavedAsync(dto);
            if (saved)
                throw new InvalidOperationException("Post already saved.");

            var savedPost = _mapper.Map<SavedPost>(dto);

            await _unitOfWork.SavedPosts.AddAsync(savedPost);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return savedPost.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task RemoveSavedPostAsync(SavedPostDto dto, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var savedPost = await _unitOfWork.SavedPosts.GetByUserAndPublicationAsync(dto.UserID, dto.PublicationID);
            if (savedPost == null)
                throw new KeyNotFoundException("Saved post not found.");

            await _unitOfWork.SavedPosts.DeleteAsync(savedPost.Id);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<bool> IsPostSavedAsync(SavedPostDto dto)
    {
        return await _unitOfWork.SavedPosts.IsPostSavedAsync(dto.UserID, dto.PublicationID);
    }
}
