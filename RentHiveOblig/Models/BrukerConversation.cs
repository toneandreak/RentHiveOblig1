using System.ComponentModel.DataAnnotations;

namespace RentHiveOblig.Models
{
    public class BrukerConversation
    {
        [Key] public int BrukerConversationId { get; set; }
        public int BrukerId { get; set; }
   //     public Bruker Bruker { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
