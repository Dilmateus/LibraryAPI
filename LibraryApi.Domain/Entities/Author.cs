using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace LibraryApi.Domain.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
