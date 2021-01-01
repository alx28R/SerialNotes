using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SerialNotes.Models;

namespace SerialNotes.Controllers
{
    public class SerialController : Controller
    {
        private ApplicationContext db;
        public SerialController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            try
            {

              var serial = db.Serials.Include(s => ((SerialsSQL)s).PartsSerials).ToList();
                return View(serial);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Response response = null;
            string htmlRender = "";
            if (id == null) return NotFound();
            try
            {
                var serial = await db.Serials.Where(s => s.SerialId == id).ToListAsync();
                htmlRender = Helper.RenderRazorViewToString(this, "Partials/DeleteModal", serial.FirstOrDefault());

                response = new Response(serial.Count, htmlRender);

            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            return Json(response);
        }


   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            IResponse response = null;

            try
            {
                response = new Response();
               var serial = (await db.Serials.Where(s => s.SerialId == id).ToListAsync()).FirstOrDefault();

               db.Serials.Remove(serial);
               db.SaveChanges();

                var list = await db.Serials.Include(s => ((SerialsSQL)s).PartsSerials).ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Index", list);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            IResponse response = new Response();

            try
            {
                if (id == null) throw new Exception("Не верынй id");
                SerialsSQL serial = (await db.Serials.Where(s => s.SerialId == id).ToListAsync()).FirstOrDefault();

                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/EditModel", serial);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Json(response);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm([Bind("SerialId", "SerialName", "ReleaseDate", "Country", "Producer", "SerialDescription")]SerialsSQL serial)
        {
            IResponse response = new Response();
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@SerialId", serial.SerialId),
                    new SqlParameter("@serialName", serial.SerialName),
                    new SqlParameter("@country", serial.Country),
                    new SqlParameter("@releaseDate", serial.ReleaseDate),
                    new SqlParameter("@producer", serial.Producer),
                    new SqlParameter("@serialDescription", serial.SerialDescription),
                };

                string proc = "SerialsUpdate @serialId, @serialName, @country, @releaseDate, @producer, @serialDescription";
                response.Status = await db.Database.ExecuteSqlRawAsync(proc, parameters);
                db.SaveChanges();
                var list = await db.Serials.Include(s => ((SerialsSQL)s).PartsSerials).ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Index", list);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }



        public async Task<IActionResult> Part([Bind("PartId", "IsViewed")]PartsSerial part)
        {
            IResponse response = new Response();
            try
            {
                PartsSerial p = (await db.PartsSerial.Where(p => p.PartId == part.PartId).ToListAsync()).FirstOrDefault();  
                if(p != null)
                {
                    p.IsViewed = part.IsViewed;
                    db.PartsSerial.Update(p);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }

        [HttpGet]
        public IActionResult AddPart(int? id)
        {
            Response response = null;
            string htmlRender = "";
            if (id == null) return NotFound();
            try
            {
                PartsSerial part = new PartsSerial() { SerialId = (int)id };
                htmlRender = Helper.RenderRazorViewToString(this, "Partials/AddPartModel", part);

                response = new Response(1, htmlRender);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }

        [HttpPost, ActionName("AddPart")]
        public async Task<IActionResult> AddPartConfirm([Bind("SerialId, P, Season, IsViewed")]PartT part)
        {
            IResponse response = new Response();
            try
            {

                List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@serialId", part.SerialId),
                    new SqlParameter("@part", part.P),
                    new SqlParameter("@season", part.Season),
                    new SqlParameter("@isViewed", part.IsViewed),
                    new SqlParameter("@partId", response.Status)
                };

                string proc = "PartsSerialInsert @serialId,@part, @season, @isViewed, @partId out";
                await db.Database.ExecuteSqlRawAsync(proc, parameters);

                var list = await db.Serials.Include(s => ((SerialsSQL)s).PartsSerials).ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Index", list);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }


        public async Task<IActionResult> DeletePart(int id)
        {
            IResponse response = new Response();
            try
            {
                PartsSerial part = (await db.PartsSerial.Where(p => p.PartId == id).ToListAsync()).FirstOrDefault();
                db.PartsSerial.Remove(part);
                db.SaveChanges();


                var list = await db.Serials.Include(s => ((SerialsSQL)s).PartsSerials).ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Index", list);

            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
            return Json(response);
        }


    }
}
