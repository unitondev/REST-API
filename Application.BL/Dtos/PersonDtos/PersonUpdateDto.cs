using System.ComponentModel.DataAnnotations;

namespace Application.BL.Dtos.PersonDtos
{
    public class PersonUpdateDto
    {
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