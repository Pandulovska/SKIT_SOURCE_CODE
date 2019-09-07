using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Display(Name="Name and Surname")]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
        public int Points { get; set; }

        public virtual List<Film> movies { get; set; }
        public Client() {
            movies = new List<Film>();
        }
    }
}