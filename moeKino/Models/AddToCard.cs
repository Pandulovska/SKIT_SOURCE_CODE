using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class AddToCard
    {
        //model za relacijata film klient t.e koj klient koi filmovi gi ima
        public int ClientId { get; set; }
        public int MovieId { get; set; }
        public List<Film> movies { get; set; }

        public AddToCard() {
            movies = new List<Film>();
        }
    }
}