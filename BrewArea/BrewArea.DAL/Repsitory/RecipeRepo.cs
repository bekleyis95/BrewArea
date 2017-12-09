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
                var recipeList = ctx.Recipes.Where(t => t.IsActive).ToList();
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

        public bool Delete(int id)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var dbItem = ctx.Recipes.Where(t => t.RecipeId == id).SingleOrDefault();
                    dbItem.IsActive = false;
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public bool Edit(Recipe recipe)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var driven = ctx.Recipes.Where(t => t.RecipeId == recipe.RecipeId).SingleOrDefault();
                    driven.Name = recipe.Name;
                    driven.Making = recipe.Making;
                    driven.Description = recipe.Description;
                    driven.BeerTypeId = recipe.BeerTypeId;
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
