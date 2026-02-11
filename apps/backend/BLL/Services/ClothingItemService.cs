using AutoMapper;
using BLL.DTO;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using DAL.Helpers;
using DAL.Helpers.Params;

namespace BLL.Services
{
    public class ClothingItemService : IClothingItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClothingItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<ClothingItemDto>> GetItemsByTypeAsync(int userId, int typeId, ClothingItemParams parameters)
        {
            var clothingItems = await _unitOfWork.ClothingItems.GetByTypeAsync(userId, typeId, parameters);
            return _mapper.Map<PagedList<ClothingItemDto>>(clothingItems);
        }

        public async Task<ClothingItemDto> GetClothingItemByIdAsync(int id)
        {
            var clothingItem = await _unitOfWork.ClothingItems.GetByIdAsync(id);
            return _mapper.Map<ClothingItemDto>(clothingItem);
        }

        public async Task<ClothingItemAllDto> GetClothingItemDetailsAsync(int id)
        {
            var clothingItem = await _unitOfWork.ClothingItems.GetClothingItemDetailsAsync(id);
            return _mapper.Map<ClothingItemAllDto>(clothingItem);
        }

        public PagedList<ClothingItemAllDto> GetAllClothingItems(ClothingItemParams parameters)
        {
            var searchFields = new List<string> { "Name" };
            var clothingItem = _unitOfWork.ClothingItems.GetAllClothingItems(parameters, searchFields);
            return _mapper.Map<PagedList<ClothingItemAllDto>>(clothingItem);
        }
        
        public async Task<Dictionary<string, List<ClothingItemDto>>> GetClothingItemsGroupedByTypeAsync(int userId)
        {
            var groupedAddOns = await _unitOfWork.ClothingItems.GetClothingItemsGroupedByTypeAsync(userId);
            var result = new Dictionary<string, List<ClothingItemDto>>();
            foreach (var k in groupedAddOns) result[k.Key] = _mapper.Map<List<ClothingItemDto>>(k.Value);
            return result;
        }

        public async Task<int> AddClothingItemAsync(CreateClothingItemDto clothingItemDto, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var clothingItem = _mapper.Map<ClothingItem>(clothingItemDto);

                await _unitOfWork.ClothingItems.AddAsync(clothingItem);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return clothingItem.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task UpdateClothingItemAsync(ClothingItemDto clothingItemDto, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var clothingItem = await _unitOfWork.ClothingItems.GetByIdAsync(clothingItemDto.Id);
                if (clothingItem == null) throw new KeyNotFoundException("Clothing item not found.");

                _mapper.Map(clothingItemDto, clothingItem);

                await _unitOfWork.ClothingItems.UpdateAsync(clothingItem);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public async Task DeleteClothingItemAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var clothingItem = await _unitOfWork.ClothingItems.GetByIdAsync(id);
                if (clothingItem == null) throw new KeyNotFoundException("Clothing item not found.");

                await _unitOfWork.ClothingItems.DeleteAsync(id);
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
}
