﻿using System;
using System.Collections.Generic;

namespace CinemaApi.Models
{
    public partial class Seat
    {
        public Seat()
        {
            Room = new HashSet<Room>();
            SeatReservation = new HashSet<SeatReservation>();
        }

        public string RowNumb { get; set; }
        public int SeatNumb { get; set; }
        public int IdSeat { get; set; }

        public virtual ICollection<Room> Room { get; set; }
        public virtual ICollection<SeatReservation> SeatReservation { get; set; }
    }
}