using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrewArea.COM;
using BrewArea.BUS.Service;
namespace BrewArea.GUI.Controllers
{
    public class RecipeController : Controller
    {
        RecipeService service = new RecipeService();

        // GET: Recipe
        public ActionResult Index()
        {
            return View(service.GetAll());
        }

        // GET: Recipe/Id
        public ActionResult Details(int id)
        {
            return View(service.GetById(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var iServ = new IngredientService();
            ViewBag.Ingredients = iServ.GetAll();
            var selectsIng = new List<SelectListItem>();
            foreach (var ingr in iServ.GetAll().Ingredients)
            {
                selectsIng.Add(new SelectListItem {
                    Text = ingr.IngredientName,
                    Value = ingr.IngredientId.ToString()
                });
            };
            ViewBag.Ingredients = selectsIng;
            return View();
        }
        [HttpPost]
        public ActionResult Create(RecipeIndexViewModel rivm)
        {
            service.CreateRecipe(rivm);
            return RedirectToAction("Index");
        }
    }
}