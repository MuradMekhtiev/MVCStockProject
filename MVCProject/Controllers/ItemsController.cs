using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models.Entity;
using PagedList;

namespace MVCProject.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        StockEntities db = new StockEntities();
        public ActionResult Index(int page = 1)
        {
            //var values = db.Items.ToList();
            var values = db.Items.ToList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddItem() 
        {
            List<SelectListItem> values = (from i in db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Item p1) 
        {
            var ctg = db.Categories.Where(m => m.CategoryID == p1.Category.CategoryID).FirstOrDefault();
            p1.Category = ctg;
            db.Items.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id) 
        {
            var item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            var item = db.Items.Find(id);

            List<SelectListItem> values = (from i in db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CategoryName,
                                               Value = i.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;

            return View("Update", item);
        }
        public ActionResult UpdateData(Item p1)
        {
            var item = db.Items.Find(p1.ItemID);
            item.ItemName = p1.ItemName;
            item.Brand = p1.Brand;
            item.Stock = p1.Stock;
            item.ItemCost = p1.ItemCost;
            var ctg = db.Categories.Where(m => m.CategoryID == p1.Category.CategoryID).FirstOrDefault();
            item.Category = ctg;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}