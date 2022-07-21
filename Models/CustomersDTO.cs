using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmailSender.Models
{
    public class CustomersDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public Guid Uid { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
