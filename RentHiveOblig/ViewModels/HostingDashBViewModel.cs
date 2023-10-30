using RentHiveOblig.Models;

namespace RentHiveOblig.ViewModels
{
    public class HostingDashBViewModel
    {
        public IEnumerable<Booking> PendingBookings { get; set; }

        public IEnumerable<Booking> ApprovedBookings { get; set; }

        public IEnumerable<Eiendom> Eiendoms { get; set; }
    }
}
