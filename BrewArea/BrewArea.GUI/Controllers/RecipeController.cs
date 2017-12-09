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
            var selectsBT = new List<SelectListItem>();
            foreach (var beerType in service.GetBeerTypes())
            {
                selectsBT.Add(new SelectListItem
                {
                    Value = beerType.BeerType1,
                    Text = beerType.BeerTypeId.ToString()
                });
            };
            ViewBag.BeerTypes = selectsBT;
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var rivm = new RecipeIndexViewModel
            {
                BeerDesc = collection["BeerDesc"],
                BeerMake = collection["BeerMake"],
                BeerType = collection["BeerType"],
                OwnerId = 1,//Degistirilecek
                OwnerNick = "DeniizBb",
                RecipeName = collection["RecipeName"]
            };

            service.CreateRecipe(rivm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(service.GetById(id));
        }
        public ActionResult DeleteWithId(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var selectsBT = new List<SelectListItem>();
            foreach (var beerType in service.GetBeerTypes())
            {
                selectsBT.Add(new SelectListItem
                {
                    Value = beerType.BeerType1,
                    Text = beerType.BeerTypeId.ToString()
                });
            };
            ViewBag.BeerTypes = selectsBT;
            return View(service.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var rivm = new RecipeIndexViewModel
            {
                RecipeId = Int32.Parse(collection["RecipeId"]),
                BeerDesc = collection["BeerDesc"],
                BeerMake = collection["BeerMake"],
                BeerType = collection["BeerType"],
                OwnerId = 1,//Degistirilecek
                OwnerNick = "DeniizBb",
                RecipeName = collection["RecipeName"]
            };

            service.EditRecipe(rivm);
            return RedirectToAction("Index");
        }
    }
}