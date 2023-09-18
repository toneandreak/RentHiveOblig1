using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        // Foreign keys for the participating users
        [Required(ErrorMessage ="A participating user(1) is required.")]
        public int User1Id { get; set; }
        [Required(ErrorMessage = "A participating user(2) is required.")]
        public int User2Id { get; set; }

        
        
        //The participating users
        public Bruker User1 { get; set; }
        public Bruker User2 { get; set; }

        // Navigation property for the messages in this conversation
        public ICollection<Message> Messages { get; set; }


        //Time
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

}
