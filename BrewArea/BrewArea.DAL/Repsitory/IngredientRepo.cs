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

        public List<IngredientViewModel> GetMemberIngredients(int memberId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                var ribmList = new List<IngredientViewModel>();
                var rirList = ctx.IngredientMemberRelations.Where(t => t.MemberId == memberId).ToList();
                foreach (var rirItem in rirList)
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

        public IngredientViewModel GetRecipeIngredientById(int recipeId, int ingredientId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var rir = ctx.RecipeIngredientRelations.Where(t => t.IngredientId == ingredientId && t.RecipeId == recipeId).SingleOrDefault();
                    var othRep = new OthersRepo();
                    return new IngredientViewModel {
                        Amount = rir.Amount,
                        IngredientName = GetById(ingredientId).Name,
                        MeasurementType = othRep.GetMeasurementType(rir.MeasurementTypeId).MeasurementType1
                    };
                }
                catch(Exception e)
                {
                    return null;
                }

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
        public bool EditIngredientToRecipe(int recipeId, int ingredientIdOld, int ingredientId, int measurementTypeId, double amount)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    DeleteIngredientFromRecipe(recipeId, ingredientIdOld);
                    AddIngredientToRecipe(recipeId, ingredientId, measurementTypeId, amount);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public bool DeleteIngredientFromRecipe(int recipeId, int ingredientId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var toDelete = ctx.RecipeIngredientRelations.Where(t => t.IngredientId == ingredientId && t.RecipeId == recipeId).SingleOrDefault();
                    if (toDelete != null)
                    {
                        ctx.RecipeIngredientRelations.Remove(toDelete);
                        ctx.SaveChanges();

                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }
        public bool AddIngredientToMember(int memberId, int ingredientId, int measurementTypeId, double amount)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    ctx.IngredientMemberRelations.Add(new IngredientMemberRelation
                    {
                        MemberId= memberId,
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
        public bool EditIngredientToMember(int memberId, int ingredientIdOld, int ingredientId, int measurementTypeId, double amount)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    DeleteIngredientFromMember(memberId, ingredientIdOld);
                    AddIngredientToMember(memberId, ingredientId, measurementTypeId, amount);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        public bool DeleteIngredientFromMember(int memberId, int ingredientId)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var toDelete = ctx.IngredientMemberRelations.Where(t => t.IngredientId == ingredientId && t.MemberId == memberId).SingleOrDefault();
                    if (toDelete != null)
                    {
                        ctx.IngredientMemberRelations.Remove(toDelete);
                        ctx.SaveChanges();

                    }
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
