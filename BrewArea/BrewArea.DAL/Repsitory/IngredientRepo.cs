using BrewArea.COM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewArea.DAL.Repsitory
{
    public class IngredientRepo
    {
        public List<Ingredient> GetAll()
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Ingredients.ToList();
            }
        }

        public Ingredient GetById(int ingredientId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Ingredients.Where(t => t.IngredientId == ingredientId).SingleOrDefault();
            }

        }

        public Ingredient GetByName(string ingredientName)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Ingredients.Where(t => t.Name == ingredientName).SingleOrDefault();
            }


        }

        public List<IngredientViewModel> GetRecipeIngredients(int recipeId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                var ribmList = new List<IngredientViewModel>();
                var rirList = ctx.RecipeIngredientRelations.Where(t => t.RecipeId == recipeId).ToList();
                foreach(var rirItem in rirList)
                {
                    var ingredientName = ctx.Ingredients.Where(t => t.IngredientId == rirItem.IngredientId).SingleOrDefault().Name;
                    var measurementType = ctx.MeasurementTypes.Where(t => t.MeasurementTypeId == rirItem.MeasurementTypeId).SingleOrDefault().MeasurementType1;
                    var amount = rirItem.Amount;
                    ribmList.Add(new IngredientViewModel
                    {
                        Amount = amount,
                        IngredientName = ingredientName,
                        MeasurementType = measurementType
                    });
                }

                return ribmList;
            }
        }

        public bool Create(Ingredient newIngredient)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    ctx.Ingredients.Add(newIngredient);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public int CreateByName(string IngredientName)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var x = ctx.Ingredients.Add(new Ingredient {
                        Name = IngredientName
                    });
                    ctx.SaveChanges();
                    return x.IngredientId;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public bool AddIngredientToRecipe(int recipeId, int ingredientId, int measurementTypeId, double amount)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var orp = new OthersRepo();
                    ctx.RecipeIngredientRelations.Add(new RecipeIngredientRelation
                    {
                        RecipeId = recipeId,
                        IngredientId = ingredientId,
                        MeasurementTypeId = measurementTypeId,
                        Amount = amount
                    });
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
