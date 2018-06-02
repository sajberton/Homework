using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WorldCup.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult LogIn(string username, string password)
        {
            if (username == "user" && password == "password")
            {
                FormsAuthentication.SetAuthCookie(username, false);
                return new RedirectResult("~/Matches/Index");
            }
            else
            {
                return new RedirectResult("~/Home/Index");
            }
        }

        [HttpGet]
        public RedirectResult LogOut()
        {
            FormsAuthentication.SignOut();
            return new RedirectResult("~/Home/Index");
        }


    }
}