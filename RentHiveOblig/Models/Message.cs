using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentHiveOblig.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        //kommentert ut fordi jeg ikke skjønner hva problemet er, men tror det er her

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        // Foreign key for the sender
        [ForeignKey("Sender")]
        public int SenderId { get; set; } //Needed to identify who the sender of the message is. Does not need a Receiver here because it is determined by the Conversation. 


        //Navigation property for the sender

        // Navigation property for the conversation
        public Conversation? Conversation { get; set; }


        // The text of the message.
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        [Required(ErrorMessage = "Content is required to send a message.")]
        public string? Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;  // Sets the default timestamp. 
    }
}

