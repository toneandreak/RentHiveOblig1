using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RentHiveOblig.Models
{


    //JOIN TABLE -- Many-to-many relationship between Wishlist and Eiendom. (Eiendom can be in many wishlist, and wishlists can have many eiendom)

    public class WishlistEiendom
    {
        public int WishListEiendomId { get; set; }
        public int WishlistId { get; set; }
        public int EiendomId { get; set; }

        public Wishlist Wishlist { get; set; }
        public Eiendom Eiendom { get; set; }
        public WishlistEiendom() { }
    }
}
