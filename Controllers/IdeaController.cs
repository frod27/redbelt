using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using redbelt.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace redbelt.Controllers {
    public class IdeaController : Controller {
        private IdeaContext _context;
        public IdeaController(IdeaContext context) {
            _context = context;
        }
// GET: /Home/
        [HttpGet]
        [Route("idea")]
        public IActionResult Idea() {
            if (HttpContext.Session.GetInt32("UserId") == 0 || HttpContext.Session.GetInt32("UserId") == null) {
                TempData["message"] = "You have to be logged in to see this page!";
                return RedirectToAction("Index", "User");
            }
            List<Idea> AllIdeas = _context.ideas
                            .Include(r => r.User)
                            .Include(y => y.participants)
                                .ThenInclude(r => r.User)
                            .ToList();

            ViewBag.AllIdeas = AllIdeas;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.Message = TempData["message"];
            return View("Idea");
        }
//add page rpute
        [HttpGet]
        [Route("addpage")]
        public IActionResult AddPage() {
            if (HttpContext.Session.GetInt32("UserId") == 0 || HttpContext.Session.GetInt32("UserId") == null) {
                TempData["message"] = "You have to be logged in to see this page!";
                return RedirectToAction("Index", "User");
            }
            // ViewBag.Message = TempData["message"];
            return View("AddPage");
        }

        [Route("page/{IdeId}")]
        public IActionResult Page(int IdeId) {
            if (HttpContext.Session.GetInt32("UserId") == 0 || HttpContext.Session.GetInt32("UserId") == null) {
                TempData["message"] = "You have to be logged in to see this page!";
                return RedirectToAction("Index", "User");
            }
            ViewBag.Idea = _context.ideas
                                .Include(r => r.User)
                                .Where(x => x.IdeaId == IdeId).SingleOrDefault();
            ViewBag.Participants = _context.participants.Where(g => g.IdeaId == IdeId).Include(u => u.User).ToList();
            ViewBag.User = _context.users.Where(g => g.UserId == HttpContext.Session.GetInt32("UserId")).SingleOrDefault();
            return View();
        }

        [HttpPost]
        [Route("/add")]
        public IActionResult Add(AddIdeaViewModel Ide) {
            if(ModelState.IsValid) {
                Idea newIdea = new Idea{
                Description = Ide.Description,
                created_at = DateTime.Now,
                updated_at = DateTime.Now,
                UserId = (int)HttpContext.Session.GetInt32("UserId")
                };
                _context.Add(newIdea);
                _context.SaveChanges();
                TempData["message"] = "Successfully added a new idea! One step closer to genius level";
            } else {
                TempData["message"] = "Unsuccessful in adding a new idea. You failed, never stop trying.";
            }
            // int UserId = HttpContext.Session.SetInt32("UserId");
            return RedirectToAction("Idea");
        }
        [Route("rsvp/{IdeId}")]
        public IActionResult RSVP(int IdeId) {
            if(ModelState.IsValid) {
                Participant James = new Participant();
                James.IdeaId = IdeId;
                James.UserId = (int)HttpContext.Session.GetInt32("UserId");
                _context.Add(James);
                _context.SaveChanges();
                TempData["message"] = "Successfully liked the idea";
            } else {
                TempData["message"] = "Unsuccessful like";
            }
            return RedirectToAction("Idea");
        }

        [Route("unrsvp/{IdeId}")]
        public IActionResult UnRSVP(int IdeId) {
            if(ModelState.IsValid) {
                int CurrId = (int)HttpContext.Session.GetInt32("UserId");
                Participant remove = _context.participants.Where(x => x.IdeaId == IdeId && x.UserId == CurrId).SingleOrDefault();
                _context.participants.Remove(remove);
                _context.SaveChanges();
                TempData["message"] = "Successfully unliked the idea";
            } else {
                TempData["message"] = "Unsuccessful in unliking";
            }
            return RedirectToAction("Idea");
        }
        [Route("delete/{IdeId}")]
        public IActionResult Delete(int IdeId) {
            if(ModelState.IsValid) {
                Idea remove = _context.ideas.Where(x => x.IdeaId == IdeId).SingleOrDefault();
                List<Participant> participant = _context.participants.Where(g => g.IdeaId == IdeId).ToList();
                foreach (var person in participant) {
                    _context.participants.Remove(person);
                    _context.SaveChanges();
                }
                _context.ideas.Remove(remove);
                _context.SaveChanges();
                TempData["message"] = "Successfully deleted a idea";
            } else {
                TempData["message"] = "Unsuccessful in deleting the idea";
            }
            return RedirectToAction("Idea");
        }
    }
}
