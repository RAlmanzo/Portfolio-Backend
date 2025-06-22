using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Dtos
{
    public class EmailRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }
    }
}
