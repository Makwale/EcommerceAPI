using System.ComponentModel.DataAnnotations;

namespace ecommerce.DTO
{
    public class UserRequest
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Username { get; set; }
    }
}
