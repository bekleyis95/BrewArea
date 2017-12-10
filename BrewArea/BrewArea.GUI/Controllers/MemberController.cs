using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrewArea.COM;
using BrewArea.BUS.Service;

namespace BrewArea.GUI.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MemberViewModel mvm)
        {
            MemberService service = new MemberService();
            var user = service.GetByUsername(mvm.Username);
            if (user != null && (user.Password == mvm.Password))
            {
                Session["Username"] = mvm.Username;
                return RedirectToAction("Index", "Recipe");
            }
            return RedirectToAction("Login", "Member");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(MemberViewModel mvm)
        {
            MemberService service = new MemberService();
            if (mvm.Username != "" && mvm.Password != "")
            {

                if (service.GetByUsername(mvm.Username) == null && service.CreateMember(mvm) == true)
                {
                    return RedirectToAction("Index", "Recipe");
                }
            }

            return RedirectToAction("SignUp", "Member");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }
    }
}