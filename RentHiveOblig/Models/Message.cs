using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentHiveOblig.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationId { get; set; }

        // Navigation property for the conversation
        public Conversation Conversation { get; set; }

        // Foreign key for the sender
        [ForeignKey("Sender")]
        public int SenderId { get; set; } //Needed to identify who the sender of the message is. Does not need a Receiver here because it is determined by the Conversation. 

        // Navigation property for the sender
        //public Bruker Sender { get; set; }



        // The text of the message.
        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        [Required(ErrorMessage = "Content is required to send a message.")]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;  // Sets the default timestamp. 
    }
}

