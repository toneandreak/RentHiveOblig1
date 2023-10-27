using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentHiveOblig.Data;
using RentHiveOblig.Models;

namespace RentHiveOblig.Controllers
{
    public class ConversationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConversationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Conversations/Create
        public IActionResult Create()
        {
            //        ViewData["User1Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost");
            //        ViewData["User2Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost");
            return View();
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversationId,User1Id,User2Id,CreatedAt,UpdatedAt")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //       ViewData["User1Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User1Id);
            //        ViewData["User2Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User2Id);
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conversation == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation == null)
            {
                return NotFound();
            }
            //         ViewData["User1Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User1Id);
            //         ViewData["User2Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User2Id);
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversationId,User1Id,User2Id,CreatedAt,UpdatedAt")] Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //        ViewData["User1Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User1Id);
            //        ViewData["User2Id"] = new SelectList(_context.Bruker, "BrukerID", "BrukerEpost", conversation.User2Id);
            return View(conversation);
        }


        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conversation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Conversation'  is null.");
            }
            var conversation = await _context.Conversation.FindAsync(id);
            if (conversation != null)
            {
                _context.Conversation.Remove(conversation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return (_context.Conversation?.Any(e => e.ConversationId == id)).GetValueOrDefault();
        }
    }
}
