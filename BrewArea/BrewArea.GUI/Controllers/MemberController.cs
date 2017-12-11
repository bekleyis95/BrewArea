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
        MemberService service = new MemberService();
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
                if(user.MemberType == 1)
                {
                    Session["Admin"] = mvm.Username;
                }
                else if(user.MemberType == 3)
                {
                    Session["Username"] = mvm.Username;

                }
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

        public ActionResult Index()
        {
            int memberId;
          
            
            if (Session["Username"] != null)
            {
                ViewBag.Id = service.GetByUsername(Session["Username"].ToString()).MemberId;
                return View(service.GetAllIngredientsofMember(Session["Username"].ToString()));

            }
            else if(Session["Admin"] != null)
            {
                ViewBag.Id = service.GetByUsername(Session["Admin"].ToString()).MemberId;
                return View(service.GetAllIngredientsofMember(Session["Admin"].ToString()));

            }
            else
            {
                return RedirectToAction("Error","NotFound", new { str = "Yalniz hata"});
            }
        }

        [HttpGet]
        public ActionResult AddIngredient(int id)
        {
            var ingServ = new IngredientService();
            var base_ = ingServ.GetAll();
            var ingredients = base_.Ingredients;
            var measurements = base_.Measurements;
            var selectIng = new List<SelectListItem>();
            var selectMea = new List<SelectListItem>();
            foreach (var ing in ingredients)
            {
                selectIng.Add(new SelectListItem
                {
                    Value = ing.IngredientName,
                    Text = ing.IngredientId.ToString()
                });
            }
            foreach (var mea in measurements)
            {
                selectMea.Add(new SelectListItem
                {
                    Value = mea.MeasurementTypeName,
                    Text = mea.MeasurementTypeId.ToString()
                });
            }
            ViewBag.Ingredients = selectIng;
            ViewBag.Measurements = selectMea;
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddIngredient(FormCollection collection)
        {
            var toPage = collection["To"].ToString();
            if (service.AddIngredientToMember(Int32.Parse(collection["Id"]), new IngredientViewModel
            {
                Amount = Double.Parse(collection["Amount"]),
                IngredientName = collection["IngredientName"],
                MeasurementType = collection["MeasurementName"]
            }))
            {
                if (toPage == "Save")
                {

                }
                else
                {

                    return RedirectToAction("AddIngredient", new { id = collection["Id"] });

                }
            }
            return RedirectToAction("Index");
        }

    }
}