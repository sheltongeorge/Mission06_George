using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mission06_George.Models;

namespace Mission6Assignment.Models
{
    public class MovieApplication
    {
        [Key]  //Ran into errors without the primary key being required right here. 
        [Required]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Categories? Category { get; set; } // ensuring the connection between the two tables to show category name


        public string Title { get; set; }
        public int Year { get; set; }
        public string? Director { get; set; } //this is shorthand for getter and setters
        public string? Rating { get; set; }
        public string Edited { get; set; }
        public string? LentTo { get; set; } // ? mark indicates not required
        public string CopiedToPlex { get; set; }
        public string? Notes { get; set; }
    }
}
