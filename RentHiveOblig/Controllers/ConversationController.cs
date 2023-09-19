using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class ConversationController : Controller
    {

        /// <summary>
        /// 
        /// The idea: 
        /// 
        /// When a "message"-type button is clicked on a user-profile or property-profile we should check if there already exists a conversation between the users.
        /// If a conversation exists, then we use that conversation. 
        /// If a conversation does not exist, we create a conversation and save it to the db.
        /// Maybe if nothing gets typed in the conversation it gets deleted after some time 3 hours? Reduce empty conversations. 
        /// 
        /// We therefore need create-, read-, update- and delete-operations. 
        /// 
        /// </summary>


        private readonly ApplicationDbContext _db;

        public ConversationController(ApplicationDbContext db)
        {
            _db = db;
        }


        //Create a conversation: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConversation(int user1Id, int user2Id)
        {
            //We should first check if a conversation between the users already exists.
            
            var convoExists = await _db.Conversation
                .FirstOrDefaultAsync(u => (u.User1Id == user1Id &&  u.User2Id == user2Id) ||
                                          (u.User1Id == user2Id && u.User2Id == user1Id)
                                    );

            if (convoExists != null) //it exists then we do not create.
            {
                // Convo already exists, we redirect to that convo.
                return View(); //Temporary return!!!!!!!!!!!!!!
            }
            else //Then we create a convo. 
            {
                //We check first if meets the requirements in the model.
                if (ModelState.IsValid)
                {
                    var newConvo = new Conversation { User1Id = user1Id, User2Id = user2Id };
                    _db.Add(newConvo);
                    await _db.SaveChangesAsync();

                    //Redirect to new convo. 
                    return View(); //Temporary return!!!!!!!!!!!
                }
                else
                {
                    //Model state not valid. 
                    return View(); //Temporary return!!!!!!!!!!
                }



            }
        }



        //Delete a empty conversation (nothing has been typed after X amount of time)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInactiveConvo()
        {
            
            var now = DateTime.Now;


            //We find all the conversations that are empty.
            var emptyConvo = await _db.Conversation
                .Include(c => c.Messages).Where(c => !c.Messages.Any())
                .ToListAsync();

            //If any emptyConversations: 
            if (emptyConvo.Any())
            {
                //We specify deeper, find those that also has been inactive for more than 3 hours. 
                var inactiveConvo = emptyConvo.Where(c => (now - c.CreatedAt).TotalMinutes > 90).ToList();

                //If there still are any, we delete those.
                if (inactiveConvo.Any())
                {
                    _db.Conversation.RemoveRange(inactiveConvo);
                    await _db.SaveChangesAsync();
                }
            }

            return Ok(); 
        }



    }
}
