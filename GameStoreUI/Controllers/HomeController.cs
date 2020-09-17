using GameStoreUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();

        }
        public ActionResult Index()
        {
            var Userid = User.Identity.GetUserId();//Currnet user id
            if (Userid != null)
            {

                var User = _context.Set<IdentityUserRole>().FirstOrDefault(x => x.UserId == Userid);
                if (User != null)
                {


                    var role = _context.Roles.FirstOrDefault(t => t.Id == User.RoleId);
                    if (role.Name == "Admin")
                    {
                        return RedirectToAction("Index", "Game", new { area = "Admin" });

                    }
                    else
                    {

                        return RedirectToAction("Index", "GameUser", new { area = "User" });
                    }
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}