using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QLNHxMVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC_QLNH.Controllers
{
    public class OderController : Controller
    {
        LatMvcQlnhContext db = new LatMvcQlnhContext();
        public IActionResult Index()
        {
            var ban = db.TbDstables.ToList();
            ViewBag.Ban = ban;
            return View();
        }


        [HttpGet]
        public IActionResult detail(string dmduocchon, string TableName)
        {
            string tb = TableName;
            var dmf = db.TbDmfoods.ToList();
            var dmfselect = db.TbDmfoods.Where(f => f.CategoryName== dmduocchon).ToList();
            var selectiddm = dmfselect.FirstOrDefault().DmfoodId;
            var dmfood = db.TbFoods.Where(f => f.DmfoodId == selectiddm).ToList();
            var in4bill = db.TbBillDetails.Where(f => f.TableName == tb).ToList();
            var name = in4bill.FirstOrDefault();


            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            ViewBag.Dmfoods = dmf;
            ViewBag.Ban = TableName;
            ViewBag.food = dmfood;
            ViewBag.date = today;
            ViewBag.in4billl = in4bill;
            ViewBag.name = name;
            ViewBag.dmdc = dmduocchon;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Detailinsert(string thucanduocchon, int soluong, string TableName, string dmduocchon, DateOnly date, string name, int sdtt)
        {

            var tb = TableName;
            var dmc = dmduocchon;

            var _dmduocchon = ViewBag.dmduocchon;
            var price = await db.TbFoods.Where(f => f.FoodName == thucanduocchon).Select(f => f.Price).FirstOrDefaultAsync();
            var upd = db.TbDstables.Where(x => x.TableName == tb).ToList().SingleOrDefault();
            var in4bill = db.TbBillDetails.FirstOrDefault(f => f.FoodName==thucanduocchon);
            if (in4bill!=null)
            {
                in4bill.Quantity=in4bill.Quantity+soluong;
                in4bill.TotalAmount=in4bill.Quantity*in4bill.Price;
            }
            else
            {
                upd.Status = "Có người";
                int totalPrice = (int)price * soluong;

                var bill = new TbBillDetail
                {
                    FoodName = thucanduocchon,
                    Quantity = soluong,
                    TotalAmount = totalPrice,
                    TableName = tb,
                    BillDate=date,
                    CustomerName=name,
                    Price=price,
                    Sdt=sdtt
                };
                
                db.TbBillDetails.Add(bill);
                db.TbDstables.Update(upd);
            }

            
            await db.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

            var redirectUrl = Url.Action(nameof(detail), "Oder", new { TableName = tb, dmduocchon = dmc });

            // Chuyển hướng đến URL mong muốn
            return Redirect(redirectUrl);
        }

        [HttpGet]
        public async Task<IActionResult> ThanhToan(string TableName)
        {
            var tb = TableName;
            var tongtien = 0;
            var tt = db.TbBillDetails.Where(x => x.TableName == tb).ToList();

            foreach (var t in tt)
            {
                tongtien = tongtien + t.TotalAmount;
            }



            var redirectUrl = Url.Action(nameof(bill), "Oder", new { price = tongtien, TableName = tb });

            // Chuyển hướng đến URL mong muốn
            return Redirect(redirectUrl);
        }

        [HttpGet]
        public IActionResult bill(string TableName, int price)
        {
            var tb = TableName;

            var tt = db.TbBillDetails.Where(x => x.TableName == tb).ToList();
            if (!tt.Any())
            {
                ViewBag.bill = tt;
                ViewBag.ban = tb;
                ViewBag.price=price;

            }
            else {
                ViewBag.date = tt.FirstOrDefault().BillDate;
                ViewBag.bill = tt;
                ViewBag.ban = tb;
                ViewBag.price=price;
            }
            

            return View();
        }

        public async Task<IActionResult> xacnhanthanhtoan(string TableName)
        {
            var tb = TableName;
            var tt = db.TbBillDetails.Where(x => x.TableName == tb).ToList();
            var upd = db.TbDstables.Where(x => x.TableName == tb).ToList().SingleOrDefault();
            upd.Status = "Trống";

            foreach(var t in tt)
            {
                var billhts = new TbBillHistory
                {
                    FoodName = t.FoodName,
                    Quantity = t.Quantity,
                    TotalAmount = t.TotalAmount,
                    TableName = tb,
                    BillDate=t.BillDate,
                    CustomerName=t.CustomerName,
                    Price=t.Price,
                    Sdt=t.Sdt
                };
                db.TbBillHistories.Add(billhts);
            }
            db.TbBillDetails.RemoveRange(tt);
            db.TbDstables.Update(upd);
            await db.SaveChangesAsync();


            var redirectUrl = Url.Action(nameof(Index), "Oder");

            // Chuyển hướng đến URL mong muốn
            return Redirect(redirectUrl);
        }

    }

}
