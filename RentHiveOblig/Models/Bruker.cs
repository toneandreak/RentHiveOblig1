using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Bruker
    {
        public int BrukerID { get; set; }

        [Required(ErrorMessage="Username is required on user.")]public string BrukerNavn { get; set; }

        [Required(ErrorMessage = "Email is required on user.")] public string BrukerEpost { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string Password { get; set; }
        
        public string? PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;

        public ICollection<Review> Reviews { get; set; } //Allows Entity Frameowrk load related reviews. 

        public ICollection<Wishlist> Wishlist { get; set; } //Allows Entity Frameowrk load related reviews. 

        public ICollection<Booking> Booking { get; set; } //Allows Entity Frameowrk load related reviews. 

        public ICollection<BrukerConversation> BrukerConversations { get; set; }

        public Bruker() { }
    }
}
