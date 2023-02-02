﻿using System.ComponentModel.DataAnnotations;

namespace CarPool.Models
{
    public class OfferRideData
    {
        public int TotalPrice { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }    
        public string Date { get; set; }
        public string StopList { get; set; }
        public int UserId { get; set;}
        public int TotalSeats { get; set; }
        public string CurrentState { get; set; }
    }
}
