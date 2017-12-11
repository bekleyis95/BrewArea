using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewArea.DAL.Repsitory;
using BrewArea.COM;

namespace BrewArea.BUS.Service
{
    public class RecipeService
    {
        RecipeRepo rp;
        IngredientRepo irp;
        MemberRepo mrp;
        OthersRepo orp;
        public RecipeService()
        {
            rp = new RecipeRepo();
            irp = new IngredientRepo();
            mrp = new MemberRepo();
            orp = new OthersRepo();
        }

        public List<RecipeIndexViewModel> GetAllForAdmin()
        {
            //Do validation here
            var list = rp.GetAllForAdmin();
            List<RecipeIndexViewModel> vmList = new List<RecipeIndexViewModel>();

            foreach (var item in list)
            {
                vmList.Add(new RecipeIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Name,
                    BeerDesc = item.Description,
                    BeerMake = item.Making,
                    BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                    OwnerNick = mrp.GetById(item.PostedBy).Username,
                    Ingredients = irp.GetRecipeIngredients(item.RecipeId)
                });
            }
            return vmList;
        }
        public List<RecipeIndexViewModel> GetAllPendingForAdmin()
        {
            //Do validation here
            var list = rp.GetAllPending();
            List<RecipeIndexViewModel> vmList = new List<RecipeIndexViewModel>();

            foreach (var item in list)
            {
                vmList.Add(new RecipeIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Name,
                    BeerDesc = item.Description,
                    BeerMake = item.Making,
                    BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                    OwnerNick = mrp.GetById(item.PostedBy).Username,
                    Ingredients = irp.GetRecipeIngredients(item.RecipeId)
                });
            }
            return vmList;
        }
        public bool IsGlobal(int recipeId)
        {
            return rp.GetById(recipeId).IsGlobal;
        }
        public bool MakeGlobal(int recipeId)
        {
            return rp.MakeGlobal(recipeId);
        }
        public List<RecipeIndexViewModel> GetAllForNonUser()
        {
            //Do validation here
            var list = rp.GetAllForNonUser();
            List<RecipeIndexViewModel> vmList = new List<RecipeIndexViewModel>();

            foreach (var item in list)
            {
                vmList.Add(new RecipeIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Name,
                    BeerDesc = item.Description,
                    BeerMake = item.Making,
                    BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                    OwnerNick = mrp.GetById(item.PostedBy).Username,
                    Ingredients = irp.GetRecipeIngredients(item.RecipeId)
                });
            }
            return vmList;
        }
        public List<RecipeIndexViewModel> GetAllForUser(int memberId)
        {
            //Do validation here
            var list = rp.GetAllForMember(memberId);
            List<RecipeIndexViewModel> vmList = new List<RecipeIndexViewModel>();

            foreach (var item in list)
            {
                vmList.Add(new RecipeIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Name,
                    BeerDesc = item.Description,
                    BeerMake = item.Making,
                    BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                    OwnerNick = mrp.GetById(item.PostedBy).Username,
                    Ingredients = irp.GetRecipeIngredients(item.RecipeId)
                });
            }
            return vmList;
        }
        public List<RecipeIndexViewModel> GetAllOfUser(int memberId)
        {
            //Do validation here
            var list = rp.GetAllRecipesOfMember(memberId);
            List<RecipeIndexViewModel> vmList = new List<RecipeIndexViewModel>();

            foreach (var item in list)
            {
                vmList.Add(new RecipeIndexViewModel
                {
                    RecipeId = item.RecipeId,
                    RecipeName = item.Name,
                    BeerDesc = item.Description,
                    BeerMake = item.Making,
                    BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                    OwnerNick = mrp.GetById(item.PostedBy).Username,
                    Ingredients = irp.GetRecipeIngredients(item.RecipeId)
                });
            }
            return vmList;
        }


        public RecipeIndexViewModel GetById(int recipeId)
        {
            //Do validation here
            var item = rp.GetById(recipeId);

            var returnItem = new RecipeIndexViewModel
            {
                RecipeId = item.RecipeId,
                RecipeName = item.Name,
                BeerDesc = item.Description,
                BeerMake = item.Making,
                BeerType = orp.GetBeerType(item.BeerTypeId).BeerType1,
                OwnerNick = mrp.GetById(item.PostedBy).Username,
                Ingredients = irp.GetRecipeIngredients(item.RecipeId)
            };

            return returnItem;
        }
        public int CreateRecipe(RecipeIndexViewModel newRecipe, bool isGlobal)
        {
            //PostedBy will come from session
            //Ingredients will be displayed
            try
            {                
              return  rp.Create(new Recipe
                {
                    Description = newRecipe.BeerDesc,
                    IsActive = true,
                    IsGlobal = isGlobal,
                    Making = newRecipe.BeerMake,
                    Name = newRecipe.RecipeName,
                    BeerTypeId = CheckAndCreateBeerType(newRecipe.BeerType),
                    PostedBy = newRecipe.OwnerId
                });
             }
            catch(Exception e)
            {
                return -1;
            }
        }
        public bool EditRecipe(RecipeIndexViewModel newRecipe)
        {
            //PostedBy will come from session
            //Ingredients will be displayed
            try
            {
                rp.Edit(new Recipe
                {
                    RecipeId = newRecipe.RecipeId,
                    Description = newRecipe.BeerDesc,
                    IsActive = true,
                    IsGlobal = true,
                    Making = newRecipe.BeerMake,
                    Name = newRecipe.RecipeName,
                    BeerTypeId = CheckAndCreateBeerType(newRecipe.BeerType),
                    PostedBy = 1
                });
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                return rp.Delete(id);
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public List<BeerType> GetBeerTypes()
        {
            var othRep = new OthersRepo();
            return othRep.GetAllBeerTypes();
        }

        public bool AddIngredientToRecipe(int recipeId, IngredientViewModel ivm)
        {
            return irp.AddIngredientToRecipe(recipeId, CheckAndCreateIngredient(ivm.IngredientName), CheckAndCreateMeasurement(ivm.MeasurementType), ivm.Amount);
        }
        public bool EditIngredientToRecipe(int recipeId, int ingredientIdOld, IngredientViewModel ivm)
        {
            return irp.EditIngredientToRecipe(recipeId, ingredientIdOld, CheckAndCreateIngredient(ivm.IngredientName), CheckAndCreateMeasurement(ivm.MeasurementType), ivm.Amount);
        }
        public bool DeleteIngredientFromRecipe(int recipeId, int ingredientId)
        {
            return irp.DeleteIngredientFromRecipe(recipeId, ingredientId);
        }
        public int CheckAndCreateBeerType(string BeerTypeName)
        {
            var othRep = new OthersRepo();
            var drivenBT = othRep.GetBeerTypetByName(BeerTypeName);
            if (drivenBT  == null)
            {
                return othRep.AddBeerType(BeerTypeName);
            }
            return drivenBT.BeerTypeId;
        }
        public int CheckAndCreateMeasurement(string MeasurementName)
        {
            var othRep = new OthersRepo();
            var drivenM = othRep.GetMeasurementByName(MeasurementName);
            if (drivenM ==null)
            {
                return othRep.AddMeasurement(MeasurementName);
            }
            return drivenM.MeasurementTypeId;
        }
        public int CheckAndCreateIngredient(string IngredientName)
        {
            var ingRep = new IngredientRepo();
            var drivenI = ingRep.GetByName(IngredientName);
            if (drivenI == null)
            {
                return ingRep.CreateByName(IngredientName);
            }
            return drivenI.IngredientId;
        }
        public int FeelingLucky(int memberId)
        {
            var globalRecipes = GetAllForUser(memberId);
            var memIngredients = irp.GetMemberIngredients(memberId);
            foreach(var item in globalRecipes)
            {
                bool isOk = true;
                foreach(var ingredient in item.Ingredients)
                {
                    var matchingIngredient = memIngredients.Where(t => t.IngredientName == ingredient.IngredientName).SingleOrDefault();
                    if (matchingIngredient == null)
                    {
                        isOk = false;
                        break;
                    }
                    
                }
                if (isOk)
                {
                    return item.RecipeId;
                }
            }
            return -1;
        }
    }
}
