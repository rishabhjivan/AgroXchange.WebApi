using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AgroXchange.WebApi.Models;
using AgroXchange.WebApi.Utils;
using AgroXchange.Data;
using AgroXchange.Data.Models;

namespace AgroXchange.WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class FarmsController : ControllerBase
    {
        [HttpGet]
        public dynamic Get(string t, double lt1, double lt2, double ln1, double ln2, double rd1, double rd2)
        {
            //Parameters: t=type, lt1,lt2=latitudes, ln1,ln2=longitudes, rd1,rd2=radii
            EFDataContext _dbContext = new EFDataContext();
            IEnumerable<Farm> farmQuery = _dbContext.Farms.AsEnumerable();
            if (t.Equals("rect"))
            {
                farmQuery = farmQuery.Where(f => f.Latitude >= lt1 && f.Latitude <= lt2 && f.Longitude >= ln1 && f.Longitude <= ln2);
            }
            else if (t.Equals("circle"))
            {
                GeoLocation loc = GeoLocation.FromDegrees(lt1, ln1);
                GeoLocation[] bounds = loc.BoundingCoordinates(rd1);
                farmQuery = _dbContext.GetFarmsWithinRadius(loc.getLatitudeInRadians(), loc.getLongitudeInRadians(), rd1, 
                    bounds[0].getLatitudeInRadians(), bounds[1].getLatitudeInRadians(), 
                    bounds[0].getLongitudeInRadians(), bounds[1].getLongitudeInRadians());
            }
            return farmQuery.Select(f => new {
                f.FarmId,
                f.FarmType,
                f.Latitude,
                f.Longitude
            });
        }

        [HttpGet("{id}", Name = "Get")]
        public dynamic Get(Guid id)
        {
            EFDataContext _dbContext = new EFDataContext();
            return _dbContext.Farms.Where(f => f.FarmId == id)
                .Select(f => new {
                    f.FarmId,
                    f.FarmType,
                    f.Latitude,
                    f.Longitude
                })
                .SingleOrDefault();
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        /*[HttpPost("test")]
        public void PostTestData()
        {
            EFDataContext _dbContext = new EFDataContext();
            Random rand = new Random();
            double minLon = 4.625092844713322;
            double maxLon = 5.910493235338322;
            double lonDiff = maxLon - minLon;
            double minLat = 6.6222094267057;
            double maxLat = 7.423639427720646;
            double latDiff = maxLat - minLat;
            for (int i = 0; i < 500; i++)
            {
                Farm newFarm = new Farm();
                newFarm.FarmId = Guid.NewGuid();
                if (rand.Next(0, 1) > 0)
                    newFarm.FarmType = FarmType.Crop;
                else
                    newFarm.FarmType = FarmType.Livestock;
                double newLon = rand.NextDouble() * lonDiff + minLon;
                double newLat = rand.NextDouble() * latDiff + minLat;
                newFarm.Latitude = newLat;
                newFarm.Longitude = newLon;
                newFarm.LatitudeRad = MapUtils.ConvertDegreesToRadians(newLat);
                newFarm.LongitudeRad = MapUtils.ConvertDegreesToRadians(newLon);
                _dbContext.Farms.Add(newFarm);
            }
            _dbContext.SaveChanges();
        }*/
    }
}
