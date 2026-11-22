using Library.Domain.DTOs; using Library.Domain.Results;
namespace Library.Domain.Contracts.Services;
public interface IReviewService
{
    ResultDto<int> Create(int userId, int bookId, int rating, string comment);
    ResultDto<bool> Update(int userId, int reviewId, int rating, string comment);
    ResultDto<bool> Delete(int userId, int reviewId);
    ResultDto<bool> Approve(int reviewId);
    ResultDto<bool> Reject(int reviewId);
    ResultDto<List<ReviewDto>> GetApprovedByBook(int bookId);
    ResultDto<List<ReviewDto>> GetPending();
    ResultDto<List<ReviewDto>> GetByUser(int userId);
}
