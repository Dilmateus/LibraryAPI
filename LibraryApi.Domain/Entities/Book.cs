using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Domain.Entities
{

    public class Book
    {
        [Key] // Define como chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Define autoincremento
        public Guid Id { get; set; }

        public string Title { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; }
    }

}
