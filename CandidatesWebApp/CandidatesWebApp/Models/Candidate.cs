using System.ComponentModel.DataAnnotations;

namespace CandidatesWebApp.Models
{
    public class Candidate
    {
        [Key]
        public int? Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(10)]
        public string ResidentialZipCode { get; set; }
    }
}