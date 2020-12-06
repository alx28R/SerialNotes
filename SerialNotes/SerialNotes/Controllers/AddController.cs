using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SerialNotes.Models;
using SerialNotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerialNotes.Controllers
{
    public class AddController : Controller
    {
        private ApplicationContext db;

        public AddController(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            SerialsVM model = new SerialsVM();
            try
            {             
                model.Serials = await db.Serials.FromSqlRaw("SerialsSelect").ToListAsync();
            }catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("NoteTitle", "SerialName", "Part", "Season", "DateAdding", "Comment", "Rating")]NotesSQL notes)
        {
            IResponse response = new Response();

            try
            {

                if (notes.Comment == null) notes.Comment = ""; 

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.AddRange(new List<SqlParameter>() {
                new SqlParameter("@serialName", notes.SerialName),
                new SqlParameter("@noteTitle", notes.NoteTitle),
                new SqlParameter("@part", notes.Part),
                new SqlParameter("@season", notes.Season),
                new SqlParameter("@dataAdding", notes.DateAdding),
                new SqlParameter("@comment", notes.Comment),
                new SqlParameter("@rating", notes.Rating)
                });

                string proc = "NotesInsert @serialName, @noteTitle, @part, @season, @dataAdding, @comment, @rating";
                response.Status = await db.Database.ExecuteSqlRawAsync(proc, parameters);
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/AddNotification", response.Status);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }
    }   
}
