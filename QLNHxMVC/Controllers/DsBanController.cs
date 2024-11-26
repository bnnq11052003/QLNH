using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLNHxMVC.Models;

namespace QLNHxMVC.Controllers
{
    public class DsBanController : Controller
    {
        private readonly LatMvcQlnhContext _context;

        public DsBanController(LatMvcQlnhContext context)
        {
            _context = context;
        }

        // GET: DsBan
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbDstables.ToListAsync());
        }

        // GET: DsBan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDstable = await _context.TbDstables
                .FirstOrDefaultAsync(m => m.TableId == id);
            if (tbDstable == null)
            {
                return NotFound();
            }

            return View(tbDstable);
        }


        private void StatusOptions()
        {
            ViewBag.SttOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Trống", Text = "Trống" },
                new SelectListItem { Value = "Có người", Text = "Có người" }
            };
        }
        // GET: DsBan/Create
        public IActionResult Create()
        {
            StatusOptions();
            return View();
        }

        // POST: DsBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TableId,TableName,Status")] TbDstable tbDstable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbDstable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Statusne = new List<SelectListItem>
            {
                new SelectListItem { Value = "Trống" },
                new SelectListItem { Value = "Có người" }
            };
            return View(tbDstable);
        }

        // GET: DsBan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDstable = await _context.TbDstables.FindAsync(id);
            if (tbDstable == null)
            {
                return NotFound();
            }
            // Kiểm tra trạng thái của bàn, chỉ cho phép chỉnh sửa nếu trạng thái là "Trống"
            //if (tbDstable.Status != "Trống")
            //{
            //    return Forbid();
            //}

            StatusOptions();
            return View(tbDstable);
        }

        // POST: DsBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TableId,TableName,Status")] TbDstable tbDstable)
        {
            if (id != tbDstable.TableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDstable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDstableExists(tbDstable.TableId))
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
            StatusOptions();
            return View(tbDstable);
        }

        // GET: DsBan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDstable = await _context.TbDstables
                .FirstOrDefaultAsync(m => m.TableId == id);
            if (tbDstable == null)
            {
                return NotFound();
            }

            return View(tbDstable);
        }

        // POST: DsBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbDstable = await _context.TbDstables.FindAsync(id);
            if (tbDstable != null)
            {
                _context.TbDstables.Remove(tbDstable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDstableExists(int id)
        {
            return _context.TbDstables.Any(e => e.TableId == id);
        }
    }
}
