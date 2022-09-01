using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using studDemo1.Models;

namespace studDemo1.Controllers
{
    public class StudentTablesController : Controller
    {
        private readonly StudentContext _context;

        public StudentTablesController(StudentContext context)
        {
            _context = context;
        }

        // GET: StudentTables
       private StudentContext db = new StudentContext();

        //[HttpGet]
        public ActionResult Index(string searchBy, string search)
        {


            //return View(_context.StudentTable.ToList());
            if (searchBy == "Name")
            {
                var data = _context.StudentTable.Where(model => model.Name == search).ToList();
                return View(data);
            }
            else if (searchBy == "Class")
            {
                int id = Convert.ToInt32(search);
                var data = _context.StudentTable.Where(model => model.Class == id).ToList();
                //var data = _context.StudentTable.ToList();
                return View(data);
            }
            else
            {
                var data = _context.StudentTable.ToList();
                return View(data);
            }

            //return Content(HttpStatusCode.BadRequest, "Not found");

        }

        private ActionResult Content(HttpStatusCode badRequest, string v)
        {
            throw new NotImplementedException();
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.StudentTable.ToListAsync());
        //    //return View(await db.StudentTable.Where(x => x.Name.Contains(searching) || searching == null).ToListAsync());
        //}

        // GET: StudentTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTable = await _context.StudentTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTable == null)
            {
                return NotFound();
            }

            return View(studentTable);
        }

        // GET: StudentTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Class")] StudentTable studentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentTable);
        }

        // GET: StudentTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTable = await _context.StudentTable.FindAsync(id);
            if (studentTable == null)
            {
                return NotFound();
            }
            return View(studentTable);
        }

        // POST: StudentTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Class")] StudentTable studentTable)
        {
            if (id != studentTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentTableExists(studentTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentTable);
        }

        // GET: StudentTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentTable = await _context.StudentTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentTable == null)
            {
                return NotFound();
            }

            return View(studentTable);
        }

        // POST: StudentTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentTable = await _context.StudentTable.FindAsync(id);
            _context.StudentTable.Remove(studentTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentTableExists(int id)
        {
            return _context.StudentTable.Any(e => e.Id == id);
        }
    }
}
