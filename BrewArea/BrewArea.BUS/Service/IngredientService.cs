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
    }
}
