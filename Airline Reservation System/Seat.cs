using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline_Reservation_System
{
    class Seat
    {
        //automatic properties
        public char Row { get; set; }
        public char Column{ get; set; }
        public string CustomerName { get; set; }

        public Seat(char row, char column, string name) {
            Row = row;
            Column = column;
            CustomerName = name;
        }
    }
}
