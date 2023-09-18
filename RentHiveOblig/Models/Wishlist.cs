namespace RentHiveOblig.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public string WishlistName { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now; //Sets the property automatically to the current date and time. 

        public Bruker Bruker { get; set; }
        public ICollection<WishlistEiendom> WishListEiendom { get; set; }

    }
}
