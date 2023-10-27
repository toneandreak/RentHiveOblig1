using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Eiendom
    {

        //Primary Key
        public int EiendomID { get; set; }


        //Foreign Key from ApplicationUser
        public string ApplicationUserId { get; set; }


        //Price per night.
        [Required(ErrorMessage = "Required to fill out the price of the listing")]
        public double PrisPerNatt { get; set; }



        //Title for the property.
        [Required(ErrorMessage = "Required to fill out Title for the listing")]
        public string Tittel { get; set; }


        //Description for the property. 
        public string? Beskrivelse { get; set; }



        //Address
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string? State { get; set; }


        //Bedrooms

        [Required(ErrorMessage = "Required to fill out how many bedrooms")]
        public int Soverom { get; set; }

        //Bathrooms
        [Required(ErrorMessage ="Required to fill out how many bathrooms")]
        public int Bad { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;



        //Might add beds too.



        //Optional images - We should rather add a image class and make a one-to-many relationship.
        //Not a scaleable option. 

        public string? Image1 { get; set; } = "/Images/PlaceholderApartmentImage.png"; //Adding a default image to the first image. 
        public string? Image2 { get; set; } 
        public string? Image3 { get; set; } 




        //NAV. PROP.
        public virtual ApplicationUser ApplicationUser { get; set; } //Enables lazyloading. 

        public ICollection<Review>? Reviews { get; set; }  //Allows Entity Frameowrk load related reviews. 

        public ICollection<WishlistEiendom>? WishlistEiendom { get; set; }//Allows Entity Frameowrk load related reviews. 


        /// <summary>
        /// The booking will be used to keep track of all bookings for the apartment. 
        /// It will also be used to find Unavailable days for the calendar. 
        /// </summary>
        public ICollection<Booking>? Bookings { get; set; }








        
        public Eiendom() { }

    }
}
