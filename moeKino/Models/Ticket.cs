using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class Ticket
    {
        public int Id { set; get; }
        public int ClientId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int NumberTickets { get; set; }
        public string Movie { get; set; }

        public Ticket() { }

        public Ticket(int ClientId, string Date, string Time, int NumberTickets, string Movie)
        {
            this.ClientId = ClientId;
            this.Date = Date;
            this.Time = Time;
            this.NumberTickets = NumberTickets;
            this.Movie = Movie;
        }
    }
}