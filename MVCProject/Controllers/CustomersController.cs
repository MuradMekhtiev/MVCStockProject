using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models.Entity;
using PagedList;

namespace MVCProject.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        StockEntities db = new StockEntities();
        public ActionResult Index(int page = 1)
        {
            //var values = db.Customers.ToList();
            var values = db.Customers.ToList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddCustomer() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer p1) 
        {
            if (!ModelState.IsValid)
            {
                return View("AddCustomer");
            }
            db.Customers.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Delete(int id) 
        {
            var customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id) 
        {
            var customer = db.Customers.Find(id);
            return View("Update", customer);
        }
        public ActionResult UpdateData(Customer p1)
        {
            var category = db.Customers.Find(p1.CustomerID);
            category.CustomerName = p1.CustomerName;
            category.CustomerSurname = p1.CustomerSurname;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}