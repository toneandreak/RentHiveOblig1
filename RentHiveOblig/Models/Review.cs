using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentHiveOblig.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage ="Property foreign id is required to review.")] public int PropertyId { get; set; } //Foreign key
        [Required(ErrorMessage = "User foreign id is required to review.")] public int GuestId { get; set; } //Foreign key 

        

        /// <summary>
        /// Gets/Sets the mandatory rating on a scale of 1-5 for the review. 
        /// </summary>

        [Required(ErrorMessage ="Rating is required on a review.")] //The minimal to write a review is to set a rating. 
        [Range(1, 5, ErrorMessage ="Rating must be between 1 and 5.")] //Specifying the range (1-5). 
        [Column(TypeName ="decimal(3,2)")] //Specifying that decimals will have up to 3 digits, i.e. 3,50.
        public decimal Rating { get; set; }

        public string? Comment { get; set; } //Comments is optional.
        public DateTime DatePosted { get; set; } = DateTime.Now; // Sets/gets the date time when the review is posted. 
        public string? ReviewImage1 { get; set; } //Images is optional. 
        public string? ReviewImage2 { get; set; } //Images is optional. 
        public string? ReviewImage3 { get; set; } //Images is optional. 


        public Bruker Bruker { get; set; }   //Many-to-one relationship with "Bruker".
        public Eiendom Eiendom { get; set; } //Many-to-one relationship with "Eiendom".


    }
}
