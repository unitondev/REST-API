using System.ComponentModel.DataAnnotations;

namespace Application.Domain.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; }
        [MaxLength(30)]
        public string PrivateInformation { get; set; }
    }
}