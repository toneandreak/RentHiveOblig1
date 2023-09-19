using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;
using System.Runtime.CompilerServices;
using System.Linq;

namespace RentHiveOblig.Controllers
{
    public class MessageController : Controller
    {



        /// <summary>
       
        ///     The Idea: 
        ///     1. Sending/Create a new message in to a spesific conversation.
       
        ///     2. List all messages in a spesific conversation
        ///     
        ///     Need create- and read-operations. (Maybe be able to update/edit an already sent message)? 
              
        /// </summary>
  

        public IActionResult Index()
        {
            return View();
        }


        private readonly ApplicationDbContext _db;

        public MessageController(ApplicationDbContext context)
        {
            _db = context; 
        }



        //Creation of a message.
        //This needs to be called whenever a user has typed something in the input field for the message and pressed the send button. 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(Message message) //We use async & await for better scalability and maintainability.
        {
            if(ModelState.IsValid) //This is checking whether the submitted data is valid and meets the requirements written in the model class. For example max-length <= 500. 
            {
                _db.Add(message);
                await _db.SaveChangesAsync(); //Commiting the creation to the corresponding database table. 
            }
            return View(message); //Temporary return. 
        }



        // GET list of messages in a conversation
        public async Task<IActionResult> ListMessages(int conversationId)
        {
         
            //We make a list of messages in the database where the messages ConversationId == to the conversationId in the parameter. 
            var messages = await _db.Message
                .Where(m => m.ConversationId == conversationId)
                .ToListAsync();


            return View(messages);
        }


    }
}
