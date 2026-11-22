namespace Library.Domain.DTOs;
public class LoanDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = "";
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public DateOnly BorrowDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public bool IsReturned { get; set; }
    public decimal FineAmount { get; set; }
}
