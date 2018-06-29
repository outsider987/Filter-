using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using demo1.Models;


namespace demol.Controllers
{
    
    [Route("api/[controller]")]
    public class Mycontroller : Controller
    {       
        KSContext   db;
        public Mycontroller(KSContext _db)
        {
            this.db = _db;
        }
        
        
        [HttpGet("/api/spots")]

        public IEnumerable<Record> Get (string k = "",string z = "",Ticketinfo y = Ticketinfo.Empty)
        {
            var data = this.db.ScenicSpots.AsQueryable();
            if(!String.IsNullOrEmpty(k))
            {
                data = data.Where(t => t.Name.Contains(k) || t.Description.Contains(k));
            }
            if(!String.IsNullOrEmpty(z))
            {
                data = data.Where(t => t.Zone ==z);
            }
            if(y!= Ticketinfo.Empty)
            {
                data = data.Where(t => t.Ticketinfo == y);
            }
            return data.Take(10);
        }
		[HttpGet("api/spots/{id}")]
        public Record GetByID (int id)
        {
            return this.db.ScenicSpots.Find(id);
        }

      // [HttpGet("/importdb")]
      // public IActionResult ImportDB()
      // {
      //     All data = All.FromJson(System.IO.File.ReadAllText("data.json"));

      //     //metho1
      //     foreach (var item in data.Result.Records)
      //     {
      //         this.db.Add(item);
      //     }
      //     //metho2
      //     this.db.AddRange (data.Result.Records);
      //     this.db.SaveChanges();
      //     return Content("OK");
      //     
      // }
    }
}
