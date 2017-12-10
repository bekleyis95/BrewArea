﻿using System;
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
        [HttpGet]
        public ActionResult AddIngredient(int id)
        {
            var ingServ = new IngredientService();
            var base_ = ingServ.GetAll();
            var ingredients = base_.Ingredients;
            var measurements = base_.Measurements;
            var selectIng = new List<SelectListItem>();
            var selectMea = new List<SelectListItem>();
            foreach(var ing in ingredients)
            {
                selectIng.Add(new SelectListItem
                {
                    Value = ing.IngredientName,
                    Text = ing.IngredientId.ToString()
                });
            }
            foreach(var mea in measurements)
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
            if (service.AddIngredientToRecipe(Int32.Parse(collection["Id"]),new IngredientViewModel {
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