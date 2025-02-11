using System.ComponentModel.DataAnnotations;

namespace Mission6Assignment.Models
{
    public class MovieApplication
    {
        [Key]  //Ran into errors without the primary key being required right here. 
        [Required]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public int Year { get; set; }
        public string Director { get; set; } //this is shorthand for getter and setters
        public string Rating { get; set; }
        public bool? Edited { get; set; }
        public string? LentTo { get; set; }
        public string? Notes { get; set; }
    }
}
