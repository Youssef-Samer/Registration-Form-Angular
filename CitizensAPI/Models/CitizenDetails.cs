using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitizensAPI.Models
{
    public class CitizenDetails
    {
        [Key]
        public Guid CitizenId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        [MaxLength(100)]
        public required string CitizenName { get; set; }

        [Column(TypeName = "nvarchar(14)")]
        [Required]
        [MaxLength(14)]
        public required string NationalId { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        [Required]
        public required string NationalIdExpirationDate { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        [Required]
        [MaxLength(12)]
        public required string SecurityCode { get; set; }

    }
}
