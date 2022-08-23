using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AuthId { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
