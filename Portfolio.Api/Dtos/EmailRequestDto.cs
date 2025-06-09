using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Dtos
{
    public class EmailRequestDto
    {
        [Required]
        [EmailAddress]
        public string From { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Message { get; set; }
    }
}
