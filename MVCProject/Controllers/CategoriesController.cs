using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCProject.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Category
        StockEntities db = new StockEntities();
        public ActionResult Index(int page = 1)
        {
            //var values = db.Categories.ToList();
            var values = db.Categories.ToList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddCategory() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category p1) 
        {
            if (!ModelState.IsValid)
            {
                return View("AddCategory");
            }
            db.Categories.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(int id) 
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int id) 
        {
            var category = db.Categories.Find(id);
            return View("Update", category);
        }

        public ActionResult UpdateData(Category p1)
        {
            var category = db.Categories.Find(p1.CategoryID);
            category.CategoryName = p1.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

//public ActionResult UpdateStudent(Student p1)
//{
//    var student = _studentDbContext.Students.Find(p1.Id);
//    student.FirstName = p1.FirstName;
//    student.LastName = p1.LastName;
//    student.Email = p1.Email;
//    _studentDbContext.SaveChanges();
//    return RedirectToAction("Index");
//}