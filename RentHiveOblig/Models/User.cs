using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class User
    {
        [Required]
        [RegularExpression(@"^[A-ZÆØÅa-zæøå][A-ZÆØÅa-zæøå '-]*$", ErrorMessage = "Invalid firstname.")]
        public string Firstname { get; set; }

        [Required]
        [RegularExpression(@"^[A-ZÆØÅa-zæøå][A-ZÆØÅa-zæøå '-]*$", ErrorMessage = "Invalid lastname.")]
        public string Lastname { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
