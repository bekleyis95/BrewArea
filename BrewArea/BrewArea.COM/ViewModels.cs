using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewArea.COM
{
    public class RecipeIndexViewModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public string BeerType { get; set; }
        public string BeerDesc { get; set; }
        public int OwnerId { get; set; }
        public string OwnerNick { get; set; }
        public string BeerMake { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }
        
        public void AddIngredient(IngredientViewModel ribm)
        {
            Ingredients.Add(ribm);
        }
    }
    public class IngredientViewModel
    {
        public double Amount { get; set; }
        public string MeasurementType { get; set; }
        public string IngredientName { get; set; }
    }

    public class IngredientSelectModel
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
    }
    public class BeerTypeSelectModel
    {
        public int BeerTypeId { get; set; }
        public string BeerTypeName { get; set; }
    }
    public class MeasurementTypeSelectModel
    {
        public int MeasurementTypeId { get; set; }
        public string MeasurementTypeName { get; set; }
    }
    public class RecipeCreateSelectViewModel
    {
        public List<IngredientSelectModel> Ingredients;
        public List<MeasurementTypeSelectModel> Measurements;
        public List<BeerTypeSelectModel> BeerTypes;

        public RecipeCreateSelectViewModel(List<Ingredient> ingredients, List<MeasurementType> measurements, List<BeerType> beerTypes)
        {

            Ingredients = new List<IngredientSelectModel>();
            Measurements = new List<MeasurementTypeSelectModel>();
            BeerTypes = new List<BeerTypeSelectModel>();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(new IngredientSelectModel
                {
                    IngredientId = ingredient.IngredientId,
                    IngredientName = ingredient.Name
                });
            }
            foreach(var measurement in measurements)
            {
                Measurements.Add(new MeasurementTypeSelectModel {
                    MeasurementTypeId = measurement.MeasurementTypeId,
                    MeasurementTypeName = measurement.MeasurementType1
                });
            }
            foreach (var beerType in beerTypes)
            {
                BeerTypes.Add(new BeerTypeSelectModel {
                    BeerTypeId = beerType.BeerTypeId,
                    BeerTypeName = beerType.BeerType1
                });
            }

        }
    }
}
