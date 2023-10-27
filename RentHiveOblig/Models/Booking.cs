using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Booking
    {

        [Key] public int BookingId { get; set; }

        [Required(ErrorMessage = "GuestId is required.")]
        public int GuestId { get; set; } //Foreign key

        [Required(ErrorMessage = "PropertyId is required.")]
        public int PropertyId { get; set; } //Foreign key

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "TotalPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = ("The value of Totalprice must be positive."))]
        public double TotalPrice { get; set; }


        public Eiendom Eiendom { get; set; }

        //Not sure what other attributes are needed atm...
    }
}
