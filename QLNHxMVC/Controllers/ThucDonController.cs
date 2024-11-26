using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

using QLNHxMVC.Models;
using X.PagedList;
using static QLNHxMVC.Controllers.ThucDonController;
namespace QLNHxMVC.Controllers
{
    public class ThucDonController : Controller
    {

        LatMvcQlnhContext db = new LatMvcQlnhContext();


        [HttpGet]
        public IActionResult Index(int? page, int iddoanhmuc)
        {

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            var foods = db.TbFoods.ToPagedList(pageNumber, pageSize);
            var doanhmucs = db.TbDmfoods.ToList();
            if (iddoanhmuc != null)
            {
                var id = db.TbFoods.Where(f => f.DmfoodId == iddoanhmuc);
                ViewBag.id = id;
                ViewBag.iddm = iddoanhmuc;
            }
            else
            {
                iddoanhmuc = 0;
                ViewBag.iddm = iddoanhmuc;
            }

            ViewBag.foods = foods;
            ViewBag.dm = doanhmucs;
            return View();
        }
        public IActionResult Create()
        {
            var doanhmucs = db.TbDmfoods.ToList();
            ViewBag.dm = doanhmucs;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodName,Price, DmfoodId")] TbFood tbFood)
        {
            if (ModelState.IsValid)
            {
                if (db.TbFoods.Any(x => x.FoodName == tbFood.FoodName && x.DmfoodId == tbFood.DmfoodId))
                {
                    ModelState.AddModelError("FoodName", "Tên món ăn đã có trong danh sách. Vui lòng đổi tên khác!");
                    var doanhmucs1 = db.TbDmfoods.ToList();
                    ViewBag.dm = doanhmucs1;
                    return View(tbFood);
                }
                db.Add(tbFood);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var doanhmucs = db.TbDmfoods.ToList();
            ViewBag.dm = doanhmucs;

            return View(tbFood);

        }
        // GET: DoanhMuc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbFood = await db.TbFoods
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (tbFood == null)
            {
                return NotFound();
            }

            return View(tbFood);
        }

        // POST: DoanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tb = await db.TbFoods.FindAsync(id);
            if (tb != null)
            {
                db.TbFoods.Remove(tb);
            }
            await db.SaveChangesAsync();
            var iddoanhmuc = id;
            /*return RedirectToAction(nameof(Index/iddoanhmuc/id));*/
            return RedirectToAction(nameof(Index));

        }
        // GET: DoanhMuc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tb = await db.TbFoods.FindAsync(id);
            var doanhmucs = db.TbDmfoods.ToList();
            ViewBag.dm = doanhmucs;

            if (tb == null)
            {
                return NotFound();
            }
            return View(tb);
        }

        // POST: DoanhMuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId, FoodName, Price, DmfoodId")] TbFood tbFood)
        {
            if (id != tbFood.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (db.TbFoods.Any(x => x.FoodName == tbFood.FoodName && x.DmfoodId == tbFood.DmfoodId))
                {
                    ModelState.AddModelError("FoodName", "Tên món ăn đã có trong danh sách. Vui lòng đổi tên khác!");
                    var doanhmucs1 = db.TbDmfoods.ToList();
                    ViewBag.dm = doanhmucs1;
                    return View(tbFood);
                }
                try
                {
                    db.Update(tbFood);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbFoodExists(tbFood.FoodId))
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
            return View(tbFood);
        }
        private bool TbFoodExists(int id)
        {
            return db.TbFoods.Any(e => e.FoodId == id);
        }
    }
}
