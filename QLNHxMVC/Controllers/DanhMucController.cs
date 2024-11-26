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
    public class DanhMucController : Controller
    {
        private readonly LatMvcQlnhContext _context;

        public DanhMucController(LatMvcQlnhContext context)
        {
            _context = context;
        }

        // GET: DoanhMuc
        public async Task<IActionResult> Index()
        {
            var tb = await _context.TbDmfoods.ToListAsync();
            ViewBag.dm = tb;
            return View();
        }

        // GET: DoanhMuc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDmfood = await _context.TbDmfoods
                .FirstOrDefaultAsync(m => m.DmfoodId == id);
            if (tbDmfood == null)
            {
                return NotFound();
            }

            return View(tbDmfood);
        }

        // GET: DoanhMuc/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: DoanhMuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DmfoodId,CategoryName")] TbDmfood tbDmfood)
        {
            if (ModelState.IsValid)
            {
                if (_context.TbDmfoods.Any(x => x.CategoryName == tbDmfood.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Tên doanh mục đã tồn tại.");
                    return View(tbDmfood);
                }
                _context.Add(tbDmfood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tbDmfood);
        }

        /*// GET: DoanhMuc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDmfood = await _context.TbDmfoods.FindAsync(id);
            if (tbDmfood == null)
            {
                return NotFound();
            }
            return View(tbDmfood);
        }

        // POST: DoanhMuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DmfoodId,CategoryName")] TbDmfood tbDmfood)
        {
            if (id != tbDmfood.DmfoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbDmfood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbDmfoodExists(tbDmfood.DmfoodId))
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
            return View(tbDmfood);
        }*/

        // GET: DoanhMuc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbDmfood = await _context.TbDmfoods
                .FirstOrDefaultAsync(m => m.DmfoodId == id);
            if (tbDmfood == null)
            {
                return NotFound();
            }

            return View(tbDmfood);
        }

        // POST: DoanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var tbDmfood = await _context.TbDmfoods.FindAsync(id);
            if (tbDmfood != null)
            {
                _context.TbDmfoods.Remove(tbDmfood);

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbDmfoodExists(int id)
        {
            return _context.TbDmfoods.Any(e => e.DmfoodId == id);
        }
    }
}
