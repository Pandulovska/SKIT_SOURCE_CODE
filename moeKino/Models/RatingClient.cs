using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class RatingClient
    {
        //model za relacijata film klient t.e koj film koi klienti gi ima i spored toa potoa ke go stavime rating-ot
        public int ClientId { get; set; }
        public int FilmId { get; set; }
        public List<Client> Clients { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public int NumberTickets { get;  set; }

        public RatingClient() {
            Clients = new List<Client>();
        }
    }
}