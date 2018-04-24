using System.ComponentModel.DataAnnotations;
using redbelt;

namespace redbelt.Models {
    public class RegisterViewModel : BaseEntity {
        [Required(ErrorMessage="Name needs to be at least 2 characters long.")]
        [MinLength(2)]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage="Name must only contain letters.")]
        public string name { get; set; }

        [Required(ErrorMessage="Alias needs to be at least 2 characters long.")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Alias must only contain letters.")]
        public string alias { get; set; }
 
        [Required(ErrorMessage="Email needs to be a valid email.")]
        [EmailAddress]
        public string email { get; set; }
 
        [Required]
        [MinLength(8, ErrorMessage="Password needs to be at least 8 characters long.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
 
        [Compare("password", ErrorMessage = "Passwords must match.")]
        public string pw_conf { get; set; }
    }
}