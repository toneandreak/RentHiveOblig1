using Microsoft.AspNetCore.Identity;

namespace RentHiveOblig.Models
{
    public class ApplicationUser : IdentityUser //Inherits from IdentityUser (Identity .Net). 

    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }




        public string? ProfilePicture { get; set; }



        //Navigation Property 

        public virtual ICollection<Eiendom> Eiendoms { get; set; }

        public ICollection<Booking>? Bookings { get; set; }


    }



}
