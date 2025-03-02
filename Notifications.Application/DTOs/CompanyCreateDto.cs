using System.ComponentModel.DataAnnotations;

namespace Notifications.Application.DTOs
{
    public class CompanyCreateDto
    {
        public Guid CompanyId { get; set; }

        [Required(ErrorMessage = "CompanyNumber is required.")]
        [MinLength(1, ErrorMessage = "CompanyNumber cannot be empty.")]
        [MaxLength(10, ErrorMessage = "CompanyNumber length cannot exceed 10 characters")]
        public string CompanyNumber { get; set; }

        [Required(ErrorMessage = "CompanyName is required.")]
        [MinLength(1, ErrorMessage = "CompanyName cannot be empty.")]
        [MaxLength(255, ErrorMessage = "CompanyName length cannot exceed 255 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "CompanyTypeCode is required.")]
        [MinLength(1, ErrorMessage = "CompanyTypeCode cannot be empty.")]
        [MaxLength(1, ErrorMessage = "CompanyTypeCode length cannot exceed 1 character")]
        public string CompanyTypeCode { get; set; }

        [Required(ErrorMessage = "CompanyMarketCode is required.")]
        [MinLength(1, ErrorMessage = "CompanyMarketCode cannot be empty.")]
        [MaxLength(2, ErrorMessage = "CompanyMarketCode length cannot exceed 2 characters")]
        public string CompanyMarketCode { get; set; }   

    }
}
