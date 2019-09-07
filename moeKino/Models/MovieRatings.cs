using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace moeKino.Models
{
    public class MovieRatings
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int movieId { get; set; }
        [Required]
        public int rating { get; set; }
        [Required]
        public int clientId { get; set; }

        public MovieRatings() {

        }
        public MovieRatings(int movieId,int rating,int clientId) {
            this.movieId = movieId;
            this.rating = rating;
            this.clientId = clientId;
        }
    }
}