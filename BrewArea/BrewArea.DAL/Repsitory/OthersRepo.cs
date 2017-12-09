using BrewArea.COM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewArea.DAL.Repsitory
{
    public class OthersRepo
    {
        public List<BeerType> GetAllBeerTypes()
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.BeerTypes.ToList();
            }
        }
        public BeerType GetBeerType(int beerTypeId)
        {
            using(var ctx = new BrewAreaEntities())
            {
                return ctx.BeerTypes.Where(t => t.BeerTypeId == beerTypeId).SingleOrDefault();
            }
        }
        public List<MeasurementType> GetAllMeasurements()
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.MeasurementTypes.ToList();
            }

        } 
        public int GetMeasurementIdtByName(string measurementType)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.MeasurementTypes.Where(t => t.MeasurementType1 == measurementType).SingleOrDefault().MeasurementTypeId;
            }
        }

    }
}
