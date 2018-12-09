﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaApi.Models;
using System.Text.RegularExpressions;

namespace CinemaApi.Controllers
{
    [Route("cinema/")]
    [ApiController]
    public class ReserveTicketController : ControllerBase
    {
        private readonly CinemaDBContext context;
        static List<string> Reservation = new List<string>();

        public ReserveTicketController(CinemaDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("GetSeats")]
        public ActionResult GetSeats()
        {
            var seatList = context.Seat.ToList();
            if (seatList.Count != 0)
                return Ok(seatList);

            return NotFound();
        }
        [HttpGet]
        [Route("GetSeat")]
        public ActionResult<SeatReservation> GetSeat(int reservation)
        {
            var seatList = context.SeatReservation.Where(a => a.IdReservation == reservation).ToList();

            if (seatList.Count != 0)
            {
                return Ok(seatList);
            }

            return NotFound();
        }
        [HttpGet]
        [Route("GetSeat2")]
        public ActionResult GetSeat2(string mask)
        {
            List<string> s = ReservedSeats(mask);
            if (s.Count != 0)
            {
                return Ok(s);
            }
            return NotFound();

        }
        [HttpGet]
        [Route("GetReserved")]
        public ActionResult<Seat> GetReserved(int idSeat)
        {
            var seatList = context.Seat.Where(a => a.IdSeat == idSeat).ToList();

            if (seatList.Count != 0)
            {
                return Ok(seatList);
            }

            return NotFound();
        }
        [HttpPost]
        [Route("AddSeat")]
        public ActionResult<int> AddSeat(int reservation, int seat)
        {
            context.SeatReservation.Add(new SeatReservation
            {
                IdSeat = seat,
                IdReservation = reservation
            });
            context.SaveChanges();

            return reservation;
        }
        [HttpPost]
        [Route("AddSeat")]
        public void AddSeat2(string seat)
        {
            Reservation.Add(seat);
          
        }
        [HttpGet]
        [Route("RemoveSeat")]
        public ActionResult RemoveSeat(int reservation, int seat)
        {
            var rm = context.SeatReservation.Where(a => a.IdReservation==reservation && a.IdSeat == seat).FirstOrDefault();

            if(rm != null)
            {
                context.SeatReservation.Remove(rm);
            }
            context.SaveChanges();

            return Ok(seat);
        }
        [HttpGet]
        [Route("RemoveSeat2")]
        public ActionResult<List<string>> RemoveSeat2(string seat)
        {
            Reservation.Remove(seat);
            return Reservation;
        }
        [HttpGet]
        [Route("MapRoom")]
        public ActionResult MapRoom(string mask)
        {
            List<string> s = MapSeats(mask);
            List<string> t = MapSeatsTaken();
            for (int i = 0; i < s.Count; i++)
            {
                for (int j = 0; j < t.Count; j++)
                {
                    if (s[i] == t[j])
                    {
                        s[i] = "Taken";
                    }
                }
            }
            if (s.Count != 0)
            {
                return Ok(s);
            }
            return NotFound();
        }
        [NonAction]

        public List<string> ReservedSeats(string mask)
        {
            Match match = Regex.Match(mask, @"[A-K]\d+");
            int matches = Regex.Matches(mask, @"[A-K]\d+").Count;
            List<string> s = new List<string>();
            for (int i = 1; i <= matches; i++)
            {
                s.Add(match.Value);
                match = match.NextMatch();

            }
            return s;
        }
        [NonAction]

        public List<string> MapSeats(string mask)
        {
            Match match = Regex.Match(mask, @"[A-K]\d*[P]*\d*[P]*\d*[Q]");
            int matches = Regex.Matches(mask, @"[A-K]\d+").Count;
            int seats = 0;
            char[] breaks;
            List<string> s = new List<string>();
            for (int i = 1; i <= matches; i++)
            {
                int x = 1;
                string mask2 = match.Value;
                var Row = Regex.Match(mask2, @"[A-K]");
                var End = Regex.Match(mask2, @"[Q]");
                var Number = Regex.Matches(mask2, @"\d+")
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
                var Space = Regex.Matches(mask2, @"[P]+")
                .OfType<Match>()
                .Select(m => m.Groups[0].Value)
                .ToArray();
                for (int j = 0; j < Number.Length; j++)
                {
                    Int32.TryParse(Number[j], out seats);
                    for (int z = 1; z <= seats; z++)
                    {
                        s.Add(Row.Value + x);
                        x++;
                    }
                    if (j != Number.Length - 1)
                    {
                        breaks = Space[j].ToCharArray();
                        for (int k = 0; k < breaks.Length; k++)
                        {
                            s.Add("P");
                        }
                    }


                }
                s.Add("Q");
                x = 1;
                match = match.NextMatch();

            }
            return s;
        }
        [NonAction]

        public List<string> MapSeatsTaken()
        {
            //List<string> reservations = new List<string>();
            string reservations="";
           // var seats = context.Reservation.Where(a => a.IdScreening == 3).ToList();
            List<string> seats = context.Reservation.Where(a => a.IdScreening == 3).Select(a=>a.SeatsReserved).ToList();
            foreach(string element in seats )
            {
                reservations += element;
            }
            
            List<string> s = ReservedSeats(reservations);
            return s;
            
        }
    }
}