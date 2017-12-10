using BrewArea.COM;
using BrewArea.DAL.Repsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewArea.BUS.Service
{
    public class IngredientService
    {
        public RecipeCreateSelectViewModel GetAll()
        {
            var ingRepo = new IngredientRepo();
            var othRepo = new OthersRepo();
            return new RecipeCreateSelectViewModel(ingRepo.GetAll(), othRepo.GetAllMeasurements(), othRepo.GetAllBeerTypes());


        }
        public IngredientViewModel GetRecipeIngredientById(int recipeId, int ingredientId)
        {
            var ingRepo = new IngredientRepo();
            return ingRepo.GetRecipeIngredientById(recipeId,ingredientId);
        }
        public int GetByName(string ingredient)
        {
            var ingRepo = new IngredientRepo();
            var ing =  ingRepo.GetByName(ingredient);
            if(ing == null)
            {
                return -1;
            }
            return ing.IngredientId;
        }
    }
}
