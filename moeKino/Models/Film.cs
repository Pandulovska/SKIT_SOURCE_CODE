using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class Film
    {
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Display(Name="Image")]
        [Required]
        public string Url { set; get; }
        [Required]
        public string Genre { set; get; }
        [Required]
        public string Director { set; get; }
        [Display(Name="Release Date")]
        [Required]
        public string ReleaseDate { set; get; }
        [Display(Name = "Short Description")]
        [Required]
        public string ShortDescription { set; get; }
        [Required]
        public string Stars { set; get; }
        public double Rating { set; get; }
        [Display(Name = "Visitors")]
        public int Audience { set; get; }
        public string Time { set; get; }
        public virtual List<Client> clients { get; set; }

        public Film() {
            clients = new List<Client>();
        }

    }
}