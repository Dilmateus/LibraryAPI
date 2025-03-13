using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Domain.Entities
{
    public class BookLoan
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;
    }
}
