using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentHiveOblig.Models
{



    public class Booking
    {

        [Key] public int BookingId { get; set; }

<<<<<<< HEAD
        [Required(ErrorMessage = "GuestId is required.")]
        public int GuestId { get; set; } //Foreign key

        [Required(ErrorMessage = "PropertyId is required.")]
        public int PropertyId { get; set; } //Foreign key

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }
=======

        [ForeignKey("ApplicationUser")]
        [Required(ErrorMessage ="GuestId is required.")]
        public int GuestId { get; set; } //Foreign key



        //The propertyId

        [ForeignKey("Eiendom")]
        [Required(ErrorMessage = "EiendomID is required.")]
        public int EiendomId { get; set; } //Foreign key
        



        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }
        


        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }
        


>>>>>>> 0d84ef777933973ea7d67925e4c4d8ddcff5996c

        [Required(ErrorMessage = "TotalPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = ("The value of Totalprice must be positive."))]
        public double TotalPrice { get; set; }



        //This will change when the host accepts the booking.
        public Boolean accepted { get; set; } = false; 





        //NAV. PROP.

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Eiendom Eiendom { get; set; }


    }
}
