using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using redbelt.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace redbelt.Controllers
{
    public class UserController: Controller
    {
        private IdeaContext _context;
        public UserController(IdeaContext context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            if(HttpContext.Session.GetInt32("UserId")>0){
                TempData["message"] = "You are logged in, can't skip home";
                return RedirectToAction("Idea","Idea");
            }
            ViewBag.Errors = new List<string>();
            ViewBag.Message = TempData["message"];
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model){
        
        if(_context.users.SingleOrDefault(u => u.Email == model.email) != null){
                TempData["message"] = "Email has already been used";
                return RedirectToAction("Index");
            }
                else
                {
                    if(ModelState.IsValid){
                           User NewUser = new User {
                        Name = model.name,
                        Alias = model.alias,
                        Email = model.email,
                        Password = model.password,
                        created_at = DateTime.UtcNow,
                        updated_at = DateTime.UtcNow,
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();
                    User current = _context.users.Where(u => u.Email == model.email).SingleOrDefault();
                    HttpContext.Session.SetInt32("UserId", current.UserId);
                    TempData["message"] = "You have successfully registered!";
                    return RedirectToAction("Idea","Idea");
                }
                ViewBag.Errors = ModelState.Values;
                ViewBag.Status = true;
                return View("Index");
            }
        }
                [Route("login")]
                public IActionResult Login (string email, string password){
                    User current = _context.users.Where(u => u.Email == email).SingleOrDefault();
                        if(current != null){
                            if(current.Password == password){
                                HttpContext.Session.SetInt32("UserId", current.UserId);
                                return RedirectToAction("Idea","Idea");
                            }
                        }
                        TempData["message"] = "Invalid Login";
                        return RedirectToAction("Index");
                }
                [Route("logout")]
                public IActionResult Logout(){
                    if(HttpContext.Session.GetInt32("UserId")>0){
                        HttpContext.Session.Clear();
                        TempData["message"] = "Successfully Logged Out";
                    }else{
                        TempData["message"] = "You weren't logged in";
                    }
                        return RedirectToAction("Index");
            }
    }
}