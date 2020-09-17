using GameStoreUI.Models;
using GameStoreUI.Models.VIewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStoreUI.Areas.Admin.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class AccountMangeController : Controller
    {
        private List<UserRolesViewModel> userroles = new List<UserRolesViewModel>();
        private ApplicationDbContext _context;

        public AccountMangeController()
        {
            _context = new ApplicationDbContext();

        }

        //public ActionResult Index(IEnumerable<UserRolesViewModel> model)
        //{

        //}





        [HttpPost]
        public ActionResult Set(int id)
        {

            var User = userroles.FirstOrDefault(x => x.Id == id);

            // SetRole(User.IdUser, User.Role);

           // return View();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        

        //[HttpPost]
        //public ActionResult Index(UserRolesViewModel user)
        //{
        //    SetRole(user.IdUser, user.Role);
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index()
        {


            //  ViewBag.Users= GetUsers();


            var users = GetUsers();
            var roles = GetRoles();
            ViewBag.Roles = RolesToString(GetRoles());
            
            foreach (var item in users.ToList())
            {
                var user = new UserRolesViewModel();

                user.User = item.Username;
                user.IdUser = item.IdUser;

                if (roles.FirstOrDefault(x => x.Id == item.IdROle) != null)
                {

                    user.Role = roles.FirstOrDefault(x => x.Id == item.IdROle).Role;

                }

                else
                {
                    user.Role = "";
                }

                userroles.Add(user);
            }







            return View(userroles);
        }

        private void SetRole(string UserId, string Role)
        {
            //   var newroleID = _context.Set<IdentityUserRole>().FirstOrDefault(x => x.RoleId == RoleId).RoleId;
            //  var newrole = _context.Roles.FirstOrDefault(x => x.Id == newroleID).Name;

            var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
            if (user == null)
            {

            }

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            if (_context.Set<IdentityUserRole>().FirstOrDefault(x => x.UserId == UserId) != null)
            {
                var oldRoleID = _context.Set<IdentityUserRole>().FirstOrDefault(x => x.UserId == UserId).RoleId;


                var oldRole = _context.Roles.FirstOrDefault(x => x.Id == oldRoleID).Name;

                // userManager.RemoveFromRole()
                var removeResult = userManager.RemoveFromRole(user.Id, oldRole);
            }
            var addresult = userManager.AddToRole(user.Id, Role);
            _context.SaveChanges();


        }

        private List<string> RolesToString(List<RolesModel> lists)
        {
            var list = new List<string>();
            foreach (var item in lists)
            {
                list.Add(item.Role);
            }
            return list;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();

            foreach (var item in _context.Users.ToList())
            {

                var role = _context.Set<IdentityUserRole>().FirstOrDefault(x => x.UserId == item.Id);
                if (role != null)
                {

                    users.Add(new UserModel
                    {
                        IdUser = item.Id,
                        Username = item.Email,
                        IdROle = role.RoleId

                    });
                }
                else
                {
                    users.Add(new UserModel
                    {
                        Username = item.Email,


                        IdROle = "No Role"

                    });
                }
            }
            return users;


        }
        public List<RolesModel> GetRoles()
        {
            List<RolesModel> roles = new List<RolesModel>();

            foreach (var item in _context.Roles.ToList())
            {

                roles.Add(new RolesModel
                {
                    Id = item.Id,
                    Role = item.Name
                });
            }
            return roles;


        }
    }
}
