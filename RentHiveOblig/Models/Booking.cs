﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentHiveOblig.Models
{



    public class Booking
    {

        [Key] public int BookingId { get; set; }


        [ForeignKey("ApplicationUser")]
        public string GuestId { get; set; } //Foreign key



        //The propertyId

        [ForeignKey("Eiendom")]
        public int EiendomId { get; set; } //Foreign key




        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime StartDate { get; set; }



        [Required(ErrorMessage = "EndDate is required.")]
        public DateTime EndDate { get; set; }




        [Required(ErrorMessage = "TotalPrice is required.")]
        [Range(0, double.MaxValue, ErrorMessage = ("The value of Totalprice must be positive."))]
        public double TotalPrice { get; set; }



        //This will change when the host accepts or declines the booking.
        public BookingStatus BookingStatus { get; set; } = BookingStatus.Pending;


        //Difference between the days
        public int QuantityDays { get; set; }


        //NAV. PROP.

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Eiendom Eiendom { get; set; }


    }

    public enum BookingStatus
    {
        Pending,
        Accepted,
        Declined
    }
}
