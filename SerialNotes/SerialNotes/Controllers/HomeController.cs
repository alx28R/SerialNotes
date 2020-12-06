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
    public class HomeController : Controller
    {
        private ApplicationContext db;

        public HomeController(ApplicationContext db)
        {
            this.db = db;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            NotesVM model = new NotesVM();
            try
            {
                model.ListNotes = await db.NotesSQL.FromSqlRaw("NotesSelect").ToListAsync();
                model.Serials = await db.Serials.FromSqlRaw("SerialsSelect").ToListAsync();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Notes()
        {
            NotesVM model = new NotesVM();
            Response response = null;
            try
            {
                response = new Response();
                model.ListNotes = await db.NotesSQL.FromSqlRaw("NotesSelect").ToListAsync();
                if (model.ListNotes != null) response.Status = 1;
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/ContentNotes", model.ListNotes);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search([Bind("Date, Text")] Search search)
        {
            NotesVM model = new NotesVM();
            string htmlRender = "";
            try
            {
                string text = search.Text == null ? "" : search.Text;

                DateTime dateStart, dateEnd;
                if (search.Date.Date == new DateTime(0001, 01, 01))
                {
                    dateStart = new DateTime(1753, 01, 01);
                    dateEnd = new DateTime(9999, 12, 31);
                }
                else
                {
                    dateStart = dateEnd = search.Date;
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.AddRange(new List<SqlParameter>() {
                new SqlParameter("@text", text),
                new SqlParameter("@dateStart", dateStart),
                new SqlParameter("@dateEnd", dateEnd)
                });

                
                model.ListNotes = await db.NotesSQL.FromSqlRaw("NotesSelectParam @text, @dateStart, @dateEnd", parameters[0], parameters[1], parameters[2]).ToListAsync();
                htmlRender = Helper.RenderRazorViewToString(this, "Partials/ContentNotes", model.ListNotes);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(new { html = htmlRender });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            List<Notes> note = new List<Notes>();
            Response response = null;
            string htmlRender = "";
            if (id == null) return NotFound();
            try
            {
                SqlParameter parameter = new SqlParameter("@id", id);


                note = await db.Notes.FromSqlRaw("NotesFindById @id", parameter).ToListAsync();
                htmlRender = Helper.RenderRazorViewToString(this, "Partials/DeleteModal", note[0]);

                response = new Response(note.Count, htmlRender);

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
                SqlParameter parameter = new SqlParameter("@id", id);
                response = new Response();
                response.Status = await db.Database.ExecuteSqlRawAsync("NotesDelete @id", parameter);

                NotesVM model = new NotesVM();
                model.ListNotes = await db.NotesSQL.FromSqlRaw("NotesSelect").ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/ContentNotes", model.ListNotes);

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
            Response response = new Response();
            List<NotesSQL> note = new List<NotesSQL>();

            try
            {
                SqlParameter parameter = new SqlParameter("@id", id);

                note = await db.NotesSQL.FromSqlRaw("NotesSelectById @id", parameter).ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/EditModal", note[0]);
                response.Status = note.Count;

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Json(response);
        }


        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm([Bind("NoteId", "NoteTitle", "SerialName", "Part", "Season", "DateAdding", "Comment", "Rating")] NotesSQL notes)
        {
            IResponse response = null;

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter ("@noteId", notes.NoteId),
                    new SqlParameter("@serialName", notes.SerialName),
                    new SqlParameter("@noteTitle", notes.NoteTitle),
                    new SqlParameter("@part", notes.Part),
                    new SqlParameter("@season", notes.Season),
                    new SqlParameter("@dataAdding", notes.DateAdding),
                    new SqlParameter("@comment", notes.Comment),
                    new SqlParameter("@rating", notes.Rating)
                };


                response = new Response();
                response.Status = await db.Database.ExecuteSqlRawAsync("NotesUpdate @noteId, @serialName, @noteTitle, @part, @season, @dataAdding, @comment, @rating", parameters);

                NotesVM model = new NotesVM();
                model.ListNotes = await db.NotesSQL.FromSqlRaw("NotesSelect").ToListAsync();
                response.RenderHtml = Helper.RenderRazorViewToString(this, "Partials/ContentNotes", model.ListNotes);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Json(response);
        }
    }
}
