namespace RentHiveOblig.Models
{
    public class Eiendom
    {
        public int EiendomID { get; set; }
        public string EiendomName { get; set; }
        public string EiendomDescription { get; set; }

        public int Pris { get; set; }
        public string Tittel { get; set; }
        public string Beskrivelse { get; set; }
        public string Sted { get; set; }
        public int Soverom { get; set; }
        public int Bad { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }


        public ICollection<Review> Reviews { get; set; }  //Allows Entity Frameowrk load related reviews. 

        public ICollection<WishlistEiendom> WishlistEiendom { get; set; }//Allows Entity Frameowrk load related reviews. 

        public Eiendom() { }
   
    }
}
