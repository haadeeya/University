using System.ComponentModel.DataAnnotations;
using Utility;

namespace Model
{
    public class User
    {
        [DataColumnName("UserId")]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        public Role Role { get; set; }

    }
}
