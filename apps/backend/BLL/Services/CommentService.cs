using AutoMapper;
using BLL.DTO.Comment;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> AddCommentAsync(CreateCommentDto dto, CancellationToken cancellationToken)
    { 
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var publication = await _unitOfWork.Publications.GetByIdAsync(dto.PublicationID);
            if (publication == null)
                throw new KeyNotFoundException("Publication not found.");
            
            if (!publication.CommentingOptions)
                throw new InvalidOperationException("Commenting is disabled for this publication.");
            
            var comment = _mapper.Map<Comment>(dto);
            comment.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            return comment.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<List<CommentDto>> GetCommentsByPublicationAsync(int publicationId)
    {
        var comments = await _unitOfWork.Comments.GetCommentsByPublicationIdAsync(publicationId);
        return _mapper.Map<List<CommentDto>>(comments);
    }
}
