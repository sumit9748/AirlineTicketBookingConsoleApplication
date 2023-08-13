using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking
{
    public class Flight
    {
        public Flight(string name,List<int>bseatArr, List<int> eseatArr,int br,int bc,int er,int ec,string s,string d) 
        {
            this.name = name;
            this.BusinessArrangement = bseatArr;
            this.EconomyArrangement= eseatArr;
            BusinessSeats = new List<List<Seat>>(br);
            for (int i = 0; i < br; i++)
            {
                List<Seat> innerSeat = new List<Seat>();
                for(int  j = 0; j < bc; j++)
                {
                    Seat seat = new Seat(i, j,2000,"Business",false);
                    innerSeat.Add(seat);
                }
                BusinessSeats.Add(innerSeat);
            }
            EconomySeats = new List<List<Seat>>(er);
            for (int i = 0; i < er; i++)
            {
                List<Seat> innerSeat = new List<Seat>();
                for (int j = 0; j < ec; j++)
                {
                    Seat seat = new Seat(i, j,1000,"Economy",false);
                    innerSeat.Add(seat);
                }
                EconomySeats.Add(innerSeat);
            }
            this.Source = s;
            this.Destination = d;
            this.AvailableSeats = bc*br+er*ec;
            this.TicketBooked = 0;
        }

        public string name { get; set; }

        public List<int> BusinessArrangement { get; set; }
        public List<int> EconomyArrangement { get; set; }

        public List<List<Seat>> BusinessSeats { get; set; }

        public List<List<Seat>> EconomySeats { get; set; }

        public int TicketBooked { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        public int AvailableSeats { get; set; }

    }
}
