using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerialNotes.Models;
using SerialNotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerialNotes.Controllers
{
    public class StatsController : Controller
    {
        private ApplicationContext db;

        public StatsController(ApplicationContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListStats(int point = 0)
        {
            try
            {
                StatsVM model = new StatsVM();
                switch (point)
                {
                    case 0:
                        model.StatsListSerial = await db.Stats.FromSqlRaw("SerialStatsAll").ToListAsync();
                        
                        break;
                    case 1:
                        model.StatsListSerial = await db.Stats.FromSqlRaw("SerialStatsMonth").ToListAsync();
                        break;
                    case 2:
                        model.StatsListSerial = await db.Stats.FromSqlRaw("SerialStatsYear").ToListAsync();
                        break;
                    case 3:
                        model.StatsListAvg = await db.StatsAvg.FromSqlRaw("SerialStatsAvgRating").ToListAsync();
                        break;
                }
                return Json(model);
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        } 
    }
}
