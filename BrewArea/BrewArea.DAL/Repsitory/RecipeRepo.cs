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

        public List<Recipe> GetAllForAdmin()
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.Where(t => t.IsActive).ToList();
                return recipeList;
            }
        }
        public List<Recipe> GetAllForNonUser()
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.Where(t => t.IsActive && t.IsGlobal).ToList();
                return recipeList;
            }
        }
        public List<Recipe> GetAllForMember(int memberId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.Where(t => t.IsActive && (t.IsGlobal || t.PostedBy == memberId)).ToList();
                return recipeList;
            }
        }
        public List<Recipe> GetAllPending()
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.Where(t => t.IsActive && !t.IsGlobal).ToList();
                return recipeList;
            }
        }
        public List<Recipe> GetAllRecipesOfMember(int memberId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipeList = ctx.Recipes.Where(t => t.IsActive && t.PostedBy == memberId).ToList();
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


        public int Create(Recipe newRecipe)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var newRec = ctx.Recipes.Add(newRecipe);
                    ctx.SaveChanges();
                    return newRec.RecipeId;
                }
                catch (Exception e)
                {
                    return -1;
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
        public bool MakeGlobal(int recipeId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                var recipe = ctx.Recipes.Where(t => t.RecipeId == recipeId).SingleOrDefault();
                if (recipe !=null )
                {
                    recipe.IsGlobal = true;
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public int GetRecipeCountForMember(int memberId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Recipes.Where(t => t.PostedBy == memberId && t.IsActive).Count();
            }
        }
        public int GetGlobalRecipeCountForMember(int memberId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.Recipes.Where(t => t.PostedBy == memberId && t.IsActive && t.IsGlobal).Count();
            }
        }

    }
}
