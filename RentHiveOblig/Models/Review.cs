namespace RentHiveOblig.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int PropertyId { get; set; } //Foreign key
        public int GuestId { get; set; } //Foreign key 
        public decimal Rating { get; set; }
        public string ?Comment { get; set; }
        public DateTime DatePosted { get; set; }   
        public string ?ReviewImage { get; set; }
        public string ?ReviewImage2 { get; set; }
        public string ?ReviewImage3 { get; set; }


        public Bruker Bruker { get; set; }   //Many-to-one relationship with "Bruker".
        public Eiendom Eiendom { get; set; } //Many-to-one relationship with "Eiendom".


    }
}
