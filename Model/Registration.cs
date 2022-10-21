using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Registration
    {

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public Role Role { get; set; }

    }
}
