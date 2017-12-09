using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewArea.COM;
using System.Threading.Tasks;

namespace BrewArea.DAL.Repsitory
{
    public class RecipeRepo
    {
        public List<Recipe> GetAll()
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.ToList();
                return recipeList;
            }
        }
        public Recipe GetById(int recipeId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Recipes.Where(t => t.RecipeId == recipeId).SingleOrDefault();
            }
        }
        public bool Create(Recipe newRecipe)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    ctx.Recipes.Add(newRecipe);
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
