using System;


namespace FlightBooking
{
    class GetFlightDetails
    {
        public void ShowDetails(string flName, List<Flight> flights)
        {
            Flight fl = flights.FirstOrDefault(f => f.name == flName);
            if (fl != null)
            {
                for (int i = 0; i < fl.BusinessSeats.Count; i++)
                {
                    for (int j = 0; j < fl.BusinessSeats[i].Count; j++)
                    {
                        Console.Write(fl.BusinessSeats[i][j].IsOccupied ? 'T' : 'F');
                    }
                    Console.WriteLine(' ');
                }

                for (int i = 0; i < fl.EconomySeats.Count; i++)
                {
                    for (int j = 0; j < fl.EconomySeats[i].Count; j++)
                    {
                        Console.Write(fl.EconomySeats[i][j].IsOccupied ? 'T' : 'F');
                    }
                    Console.WriteLine(' ');
                }
            }
            else
            {
                Console.WriteLine("Sorry No flights found");
            }
        }

        public void filterBooking(List<Flight> flights)
        {
            Console.WriteLine("1>Filter flights from given location (input as source and destination)");
            Console.WriteLine("2>Filter flights who is having business class alone ");
            Console.WriteLine("3>Display empty seats");
            Console.WriteLine("Display seats who order meal");

            int yc=Convert.ToInt32(Console.ReadLine());
            switch (yc)
            {
                case 1:
                    {
                        string s, d;
                        Console.WriteLine("enter source & destination");
                        s=Console.ReadLine();
                        d=Console.ReadLine();
                        IEnumerable<Flight> fl= flights.Where(f=>f.Source==s && f.Destination==d).ToList();
                        if (fl.Count() != 0)
                        {
                            foreach (Flight flp in fl)
                            {
                                Console.WriteLine("{0}", flp.name);
                            }
                        }

                        break;
                    }
                case 2:
                    {
                        IEnumerable<Flight> fl = flights.Where(f => f.EconomySeats.Count == 0);
                        if (fl.Count() != 0)
                        {
                            foreach (Flight flp in fl)
                            {
                                Console.WriteLine("{0}", flp.name);
                            }
                        }
                        break;

                    }
                case 3:
                    {
                        Console.WriteLine("Enter the name of the flight for which you want to see seats??");
                        string fname = Console.ReadLine();
                        Flight fl = flights.FirstOrDefault(f => f.name == fname);

                        Console.WriteLine("Business class empty seats");

                        for (int i = 0; i < fl.BusinessSeats.Count; i++)
                        {
                            for (int j = 0; j < fl.BusinessSeats[i].Count; j++)
                            {
                                if (!fl.BusinessSeats[i][j].IsOccupied)
                                    Console.Write("{0}_{1}", i, (char)j + 65); 
                                else Console.Write("{0}", "F");

                            }
                            Console.WriteLine(' ');

                        }

                        Console.WriteLine("Economy class empty seats");

                        for (int i = 0; i < fl.EconomySeats.Count; i++)
                        {
                            for (int j = 0; j < fl.EconomySeats[i].Count; j++)
                            {
                                if (!fl.EconomySeats[i][j].IsOccupied)
                                    Console.Write("{0}_{1}", i, (char)j + 65);
                                else Console.Write("{0}", "F");

                            }
                            Console.WriteLine(' ');

                        }
                        break;

                    }

                case 4:
                    {
                        Console.WriteLine("Enter the name of the flight for which you want to see meal ordering??");
                        string fname = Console.ReadLine();
                        Flight fl = flights.FirstOrDefault(f => f.name == fname);

                        for (int i = 0; i < fl.BusinessSeats.Count; i++)
                        {
                            for (int j = 0; j < fl.BusinessSeats[i].Count; j++)
                            {
                                if (fl.BusinessSeats[i][j].MealPreference)
                                    Console.Write("{0}_{1}", i,(char)j + 65);
                                else Console.Write("{0}", "F");

                            }
                            Console.WriteLine(' ');

                        }
                        break;
                    }
            }
        }
    }

    public class BookFlight
    {
        public void Book(ref List<Booking> bookings, ref List<Flight>flights) 
        {
            Console.WriteLine("Enter the name of the flight you want to book??");
            string fname = Console.ReadLine();
            Flight f = flights.FirstOrDefault(f => f.name == fname);
            flights.Remove(f);
            Dictionary<int, int> seatPref = new Dictionary<int, int>();

            Console.WriteLine("How many tickets you want to book??");
            int tnum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("What type of seats you want to book??(1.Business Rs:-2000 2.Economy Rs:-1000)");
            string type=Console.ReadLine();

            int seatPrice = 0;
            if (type == "Business")
            {
                int sum = f.BusinessArrangement[0];
                seatPref[0] = 1;

                for (int i = 1; i < f.BusinessArrangement.Count; i++)
                {

                    seatPref[sum - 1] = 1;
                    seatPref[sum] = 1;
                    sum += f.BusinessArrangement[i];
                }
                seatPrice = 2000 + (f.TicketBooked * 200);

            }
            else
            {
                int sum = f.EconomyArrangement[0];
                seatPref[0] = 1;

                for (int i = 1; i < f.EconomyArrangement.Count; i++)
                {

                    seatPref[sum - 1] = 1;
                    seatPref[sum] = 1;
                    sum += f.EconomyArrangement[i];
                }
                seatPrice = 1000 + (f.TicketBooked * 100);
            }
            for(int i = 0; i < tnum; i++)
            {
                Console.WriteLine("Enter your seatPreference( Example 5_F represents 5th row, 6th Column)");

                string bc=Console.ReadLine();

                string[] parts = bc.Split('_');
                int rNo = Convert.ToInt32(parts[0]);
                int cNo= Convert.ToChar(parts[1])-'A';
                
                if (seatPref.ContainsKey(cNo)) { seatPrice += 100; }

                Booking b = new Booking(bookings.Count + 1, seatPrice,f.name);

                bookings.Add(b);
                if (type == "Business")
                {
                    if(f.BusinessSeats[rNo][cNo].IsOccupied == true)
                    {
                        Console.WriteLine("Sorry it is already booked");
                        continue;
                    }
                    f.BusinessSeats[rNo][cNo].BookingId = b.Id;
                    f.BusinessSeats[rNo][cNo].IsOccupied = true;
                    Console.WriteLine("Do you want meal??");
                    bool mealPref = Convert.ToBoolean(Console.ReadLine());

                    f.BusinessSeats[rNo][cNo].MealPreference = mealPref ? true : false;
                }
                else
                {
                    if (f.EconomySeats[rNo][cNo].IsOccupied == true)
                    {
                        Console.WriteLine("Sorry it is already booked");
                        continue;
                    }
                    f.EconomySeats[rNo][cNo].BookingId = b.Id;
                    f.EconomySeats[rNo][cNo].IsOccupied = true;
                    Console.WriteLine("Do you want meal??");
                    bool mealPref = Convert.ToBoolean(Console.ReadLine());

                    f.EconomySeats[rNo][cNo].MealPreference = mealPref ? true : false;
                }
                Console.WriteLine("{0},{1}", rNo, cNo);
                f.AvailableSeats -= 1;
                f.TicketBooked += 1;
               
            }
            flights.Add(f);
        }

        public void SeeAllBooking(List<Booking> bookings)
        {
            for(int i=0;i<bookings.Count;i++)
            {
                Console.WriteLine("Booking Id:-{0}----Booking Cost{1}", bookings[i].Id, bookings[i].BookingPrice);
            }
        }

        public void SeeAparticularBooking(List<Booking> bookings,List<Flight> flights)
        {
            Console.WriteLine("Enter booking Id");
            int en = Convert.ToInt32(Console.ReadLine());

            Booking b=bookings.FirstOrDefault(f=>f.Id == en);
            Flight fl = flights.FirstOrDefault(f => f.name == b.flightName);
            Seat ss=null;
            for (int i = 0; i < fl.BusinessSeats.Count; i++)
            {
                for (int j = 0; j < fl.BusinessSeats[i].Count; j++)
                {
                    if (fl.BusinessSeats[i][j].BookingId == en)
                    {
                        ss = fl.BusinessSeats[i][j];
                        break;
                    }
                }
            }

            Console.WriteLine("Flight Name:-{0}", fl.name);
            if(ss!=null)
            Console.WriteLine("Flight Seat Meal Prefference:-{0}", ss.MealPreference);

            Console.WriteLine("Booking price:-{0}", b.BookingPrice);
        }
        class Program
    {
        public static List<Flight> flights = new List<Flight>();
        public static List<Booking> bookings = new List<Booking>();
        
        public static void Main(string[] args)
        {


            bool continuation = true;
            while (continuation)
            {
                GetFlightDetails gp =new GetFlightDetails();
                BookFlight bp = new BookFlight();
                Console.WriteLine("1.Add new flight Details");
                Console.WriteLine("2.See flight seats");
                Console.WriteLine("3.Search flight seats");
                Console.WriteLine("4.Book flight seats");
                Console.WriteLine("5.See All booking");
                Console.WriteLine("6.See a particular booking");
                
                

                int ch = Convert.ToInt32(Console.ReadLine());

                    switch (ch)
                    {
                        case 1:
                            {
                                Console.WriteLine("Enter flight Name");
                                string fname = Console.ReadLine();
                                Console.WriteLine("Enter source");
                                string sName = Console.ReadLine();
                                Console.WriteLine("Enter destination");
                                string dName = Console.ReadLine();

                                int TotalSeats = 0;
                                Console.WriteLine("Enter Business seat Number");
                                int bseatCol = Convert.ToInt32(Console.ReadLine());
                                int bseatRow = Convert.ToInt32(Console.ReadLine());
                                List<int> bseatArr = new List<int>();
                                for (int i = 0; i < bseatCol; i++)
                                {
                                    int ss = Convert.ToInt32(Console.ReadLine());
                                    TotalSeats += ss;
                                    bseatArr.Add(ss);
                                }
                                bseatCol = TotalSeats;
                                TotalSeats = 0;
                                Console.WriteLine("Enter Economy seat Number");
                                int eseatCol = Convert.ToInt32(Console.ReadLine());
                                int eseatRow = Convert.ToInt32(Console.ReadLine());
                                List<int> eseatArr = new List<int>();
                                for (int i = 0; i < eseatCol; i++)
                                {
                                    int ss = Convert.ToInt32(Console.ReadLine());
                                    TotalSeats += ss;
                                    eseatArr.Add(ss);
                                }
                                eseatCol = TotalSeats;
                                Flight fl = new Flight(fname, bseatArr, eseatArr, bseatRow, bseatCol, eseatRow, eseatCol, sName, dName);
                                flights.Add(fl);

                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Enter flight Name");
                                string flName = Console.ReadLine();
                                gp.ShowDetails(flName, flights);
                                break;
                            }
                        case 3:
                            {
                                gp.filterBooking(flights);
                                break;

                            }
                        case 4:
                            {

                                bp.Book(ref bookings, ref flights);
                                break;
                            }
                        case 5:
                            {
                                bp.SeeAllBooking(bookings);
                                break;
                            }
                        case 6:
                            {
                                bp.SeeAparticularBooking(bookings, flights);
                                break;
                            }
                    }
                        
                }
            }
        }
    }
}

