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

        public List<RecipeIndexViewModel> GetAll()
        {
            //Do validation here
            var list = rp.GetAll();
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
        public bool CreateRecipe(RecipeIndexViewModel newRecipe)
        {
            //PostedBy will come from session
            //Available BeerTypes will be provided
            //Ingredients will be displayed
            try
            {
                rp.Create(new Recipe
                {
                    Description = newRecipe.BeerDesc,
                    IsActive = true,
                    IsGlobal = true,
                    Making = newRecipe.BeerMake,
                    Name = newRecipe.RecipeName,
                    BeerTypeId = 1,
                    PostedBy = 1
                });
                return true;
             }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
