using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Eiendom
    {
        public int EiendomID { get; set; }

        [Required(ErrorMessage = "Required to fill out the price of the listing")]
        public double PrisPerNatt { get; set; }

        [Required(ErrorMessage = "Required to fill out Title for the listing")]
        public string Tittel { get; set; }
        public string? Beskrivelse { get; set; }
        public string Sted { get; set; }

        [Required(ErrorMessage = "Required to fill out how many bedrooms")]
        public int Soverom { get; set; }
        [Required(ErrorMessage ="Required to fill out how many bathrooms")]
        public int Bad { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string? Image1 { get; set; } //Optional images.
        public string? Image2 { get; set; } //Optional images.
        public string? Image3 { get; set; } //Optional images.


        public ICollection<Review> Reviews { get; set; }  //Allows Entity Frameowrk load related reviews. 

        public ICollection<WishlistEiendom> WishlistEiendom { get; set; }//Allows Entity Frameowrk load related reviews. 

        public ICollection<Booking> Bookings { get; set; }

        public Eiendom() { }
   
    }
}
