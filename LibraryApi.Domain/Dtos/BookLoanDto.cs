using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Domain.Dtos
{
    public class BookLoanDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}
