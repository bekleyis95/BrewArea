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
        public MeasurementType GetMeasurementByName(string measurementType)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.MeasurementTypes.Where(t => t.MeasurementType1 == measurementType).SingleOrDefault();
            }
        }
        public BeerType GetBeerTypetByName(string BeerType)
        {
            using (var ctx = new BrewAreaEntities())
            {
                return ctx.BeerTypes.Where(t => t.BeerType1 == BeerType).SingleOrDefault();
            }
        }
        public MeasurementType GetMeasurementType(int measurementTypeId)
        {

            using (var ctx = new BrewAreaEntities())
            {
                return ctx.MeasurementTypes.Where(t => t.MeasurementTypeId == measurementTypeId).SingleOrDefault();
            }
        }
        public int AddBeerType(string BeerType)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var x = ctx.BeerTypes.Add(new BeerType
                    {
                        BeerType1 = BeerType
                    });
                    ctx.SaveChanges();
                    return x.BeerTypeId;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
        public int AddMeasurement(string MeasurementType)
        {
            using (var ctx = new BrewAreaEntities())
            {
                try
                {
                    var x = ctx.MeasurementTypes.Add(new MeasurementType
                    {
                        MeasurementType1 = MeasurementType
                    });
                    ctx.SaveChanges();
                    return x.MeasurementTypeId;
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
        }
    }
}
