using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models.Entity;

namespace MVCProject.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        StockEntities db = new StockEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NewSale() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewSale(Sale p1) 
        {
            db.Sales.Add(p1);
            db.SaveChanges();
            return View("Index");
        }
    }
}