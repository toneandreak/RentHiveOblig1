using RentHiveOblig.Models;

namespace RentHiveOblig.ViewModels
{
    public class BookingRequestViewModel
    {
        public Booking Booking { get; set; }
        public Eiendom Eiendom { get; set; }

        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }

       

    }

}
