using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Eiendom
    {
        public int EiendomID { get; set; }

        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "Required to fill out the price of the listing")]
        public double PrisPerNatt { get; set; }

        [Required(ErrorMessage = "Required to fill out Title for the listing")]
        public string Tittel { get; set; }
        public string? Beskrivelse { get; set; }

        //Address
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string? State { get; set; }


        [Required(ErrorMessage = "Required to fill out how many bedrooms")]
        public int Soverom { get; set; }
        [Required(ErrorMessage = "Required to fill out how many bathrooms")]
        public int Bad { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;




        //Optional images - We should rather add a image class and make a one-to-many relationship.
        //THIS IS A NON-SCALEABLE OPTION 

        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }



        public virtual ApplicationUser ApplicationUser { get; set; } //Enables lazyloading. 

        public ICollection<Review>? Reviews { get; set; }  //Allows Entity Frameowrk load related reviews. 

        public ICollection<WishlistEiendom>? WishlistEiendom { get; set; }//Allows Entity Frameowrk load related reviews. 

        public ICollection<Booking>? Bookings { get; set; }




        public Eiendom() { }

    }
}
