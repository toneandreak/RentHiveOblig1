namespace RentHiveOblig.ViewModels
{
    public class EiendomViewModel
    {
        public double PrisPerNatt { get; set; }
        public string Tittel { get; set; }
        public string? Beskrivelse { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string? State { get; set; }
        public int Bad { get; set; }
        public int Soverom { get; set; }
    }
}
