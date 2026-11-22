using Library.Domain.DTOs;
using Library.Domain.Entities;

namespace Library.Domain.Contracts.Repositories;

public interface IReviewRepository
{
    Review? GetById(int id);
    List<ReviewDto> GetApprovedByBook(int bookId);
    List<ReviewDto> GetPending();
    List<ReviewDto> GetByUser(int userId);
    bool ExistsByUserAndBook(int userId, int bookId);
    void Add(Review review);
    void Delete(Review review);
}
