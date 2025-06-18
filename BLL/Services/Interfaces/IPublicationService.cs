using BLL.DTO.Publication;
using DAL.Entities;

namespace BLL.Services.Interfaces;
using DAL.Helpers;
using DAL.Helpers.Params;

public interface IPublicationService
{
    PagedList<PublicationListDto> GetAllPublication(PublicationParams parameters);
    Task<PublicationDetailsDto?> GetPublicationDetailsAsync(int id);
    Task<PagedList<PublicationListDto>> GetByUserAsync(int userId, PublicationParams parameters);
    Task<int> CreatePublicationAsync(PublicationDto dto, CancellationToken cancellationToken);
    Task UpdatePublicationAsync(int id, UpdatePublicationDto publicationDto, CancellationToken cancellationToken);
    Task DeletePublicationAsync(int id, CancellationToken cancellationToken);
}