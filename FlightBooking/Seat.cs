using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking
{
    public class Seat
    {
        public Seat(int r,int c,int p,string t,bool m)
        {
            this.Row = r;
            this.Column = c;
            this.IsOccupied = false;
            this.isCancled = false;
            this.BookingId = -1;
            this.Price = p;
            this.Type = t;
            this.MealPreference = m;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsOccupied { get; set; }

        public bool isCancled { get; set; }

        public int BookingId { get; set; }

        public int Price { get; set; }
        public string Type { get; set; }

        public bool MealPreference { get; set; }
    }
}
