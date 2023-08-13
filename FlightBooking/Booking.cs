using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking
{
    public class Booking
    {
        public Booking(int id,int price,string flName) 
        { 
            this.Id = id;
            this.BookingPrice= price;
            this.flightName= flName;
        }    
        public int Id { get; set; }
        public int BookingPrice { get; set; }

        public string flightName { get; set; }
    }
}
