using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        ApplicationDbContext context;
        public UserController()
        {
            context = new ApplicationDbContext();
        }

        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged In";
            }
            return View();
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public ActionResult UsersWithRoles()
        {
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new ManageUsersViewModel()

                                  {
                                      UserId = p.UserId,
                                      UserName = p.Username,
                                      Email = p.Email,
                                      UserRole = string.Join(",", p.RoleNames)
                                  });


            return View(usersWithRoles);
        }

        public ActionResult ListUsers()
        {
            return View(context.Users.ToList());
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var user = context.Users.FirstOrDefault(c => c.Id == id);
            var roles = context.Roles.ToList();
            var viewModel = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                UserRoleId = roles.FirstOrDefault(r => r.Id == user.Roles.First().RoleId).ToString(),
                Roles = roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(),

                }).ToList()

            };
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == model.UserId);
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                user.UserName = model.UserName;
                user.Email = model.Email;

                db.SaveChanges();

            }

            return RedirectToAction("UsersWithRoles");
        }
            
    }
}