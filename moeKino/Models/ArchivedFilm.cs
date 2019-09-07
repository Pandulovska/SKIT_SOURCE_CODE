using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class ArchivedFilm
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Display(Name = "Image")]
        public string Url { set; get; }
        public string Genre { set; get; }
        public string Director { set; get; }
        [Display(Name = "Release Date")]
        public string ReleaseDate { set; get; }
        [Display(Name = "Last Screening")]
        public string LastScreening { set; get; }
        [Display(Name = "Visitors")]
        public int Audience { set; get; }

        public ArchivedFilm() { }
        public ArchivedFilm(string Name,string Url,string Genre,string Director,string ReleaseDate, string LastScreening,int Audience)
        {
            this.Name = Name;
            this.Url = Url;
            this.Genre = Genre;
            this.Director = Director;
            this.ReleaseDate = ReleaseDate;
            this.LastScreening = LastScreening;
            this.Audience = Audience;
        }
    }
}