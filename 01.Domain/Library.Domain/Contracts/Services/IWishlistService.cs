using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface IWishlistService
{
    ResultDto<int> Add(int userId, int bookId);
    ResultDto<bool> Remove(int userId, int wishlistId);
    ResultDto<List<WishlistDto>> GetByUser(int userId);
}
