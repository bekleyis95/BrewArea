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
        MemberService memberService = new MemberService();
        // GET: Recipe
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                return View(service.GetAllForUser(memberService.GetByUsername(Session["Username"].ToString()).MemberId.GetValueOrDefault()));
            }
            else if (Session["Admin"] != null)
            {
                return View(service.GetAllForAdmin());
            }
            else
            {
                return View(service.GetAllForNonUser());
            }
        }

        // GET: Recipe/Id
        public ActionResult Details(int id)
        {
            ViewBag.IsPending = false;
            if (!service.IsGlobal(id) && Session["Admin"] != null)
            {
                ViewBag.IsPending = true;
            }
            return View(service.GetById(id));
        }

        public ActionResult MakeGlobal(int id)
        {
            service.MakeGlobal(id);
            return RedirectToAction("Index");
        }


        public ActionResult FeelingLucky(int memberId)
        {
            var fl = service.FeelingLucky(memberId);
            if (fl != -1)
            {
                return RedirectToAction("Details", new { id = fl });
            }
            else
            {
                return RedirectToAction("Error", "NotFound", new {str = "No matching recipes" });
            }
            
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
            int ret = -1;
            var memSrv = new MemberService();
            if(Session["Username"] != null)
            {
                var rivm = new RecipeIndexViewModel
                {
                    BeerDesc = collection["BeerDesc"],
                    BeerMake = collection["BeerMake"],
                    BeerType = collection["BeerType"],
                    OwnerId = memberService.GetByUsername(Session["Username"].ToString()).MemberId.GetValueOrDefault(),
                    OwnerNick = Session["Username"].ToString(),
                    RecipeName = collection["RecipeName"]
                };
                ret = service.CreateRecipe(rivm,false);
            }
            else if(Session["Admin"] != null)
            {
                var rivm = new RecipeIndexViewModel
                {
                    BeerDesc = collection["BeerDesc"],
                    BeerMake = collection["BeerMake"],
                    BeerType = collection["BeerType"],
                    OwnerId = memberService.GetByUsername(Session["Admin"].ToString()).MemberId.GetValueOrDefault(),
                    OwnerNick = Session["Admin"].ToString(),
                    RecipeName = collection["RecipeName"]
                };
                ret = service.CreateRecipe(rivm, true);
            }

            if(collection["To"] == "Create" || ret == -1) // error sayfasına döndür
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddIngredient",new {id = ret });
            }

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
            if (service.AddIngredientToRecipe(Int32.Parse(collection["Id"]), new IngredientViewModel
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

        [HttpGet]
        public ActionResult EditIngredient(int recipeId, string ingredient)
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
            var ingredientId = ingServ.GetByName(ingredient);
            ViewBag.recipeId = recipeId;
            ViewBag.ingredientId = ingredientId;
            return View(ingServ.GetRecipeIngredientById(recipeId, ingredientId));
        }
        [HttpPost]
        public ActionResult EditIngredient(FormCollection collection)
        {
            var ingServ = new IngredientService();
            if (service.EditIngredientToRecipe(Int32.Parse(collection["recipeId"]), Int32.Parse(collection["ingredientId"]), new IngredientViewModel
            {
                Amount = Double.Parse(collection["Amount"]),
                IngredientName = collection["IngredientName"],
                MeasurementType = collection["MeasurementName"]
            }))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");//Error
        }
        public ActionResult DeleteIngredientFromRecipe(int recipeId, string ingredient)
        {
            var ingSrv = new IngredientService();
            var ingredientId = ingSrv.GetByName(ingredient);
            service.DeleteIngredientFromRecipe(recipeId, ingredientId);
            return RedirectToAction("Edit", new { id = recipeId});
        }
    }
}