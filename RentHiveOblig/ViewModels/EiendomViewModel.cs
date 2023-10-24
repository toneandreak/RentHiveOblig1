using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.ViewModels
{
    public class EiendomViewModel
    {

        [Range(0,(double)decimal.MaxValue,
               ErrorMessage = "Please enter valid number")]
        public double PrisPerNatt { get; set; }

        [Required(ErrorMessage = "A title for the listing is required")]
        public string Tittel { get; set; }

        [Required(ErrorMessage = "A description text for the listing is required")]
        public string? Beskrivelse { get; set; }

        [Required(ErrorMessage = "Street address is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "ZipCode is required.")]
        public string ZipCode { get; set; }

        public string? State { get; set; }


        [Required(ErrorMessage = "Filling out number of bathroom is required.")]
        [Range(0, 30)]
        public int Bad { get; set; }

        [Required(ErrorMessage = "Filling out number of bedrooms is required.")]
        [Range(0, 30)]
        public int Soverom { get; set; }
    }
}
