using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Foodie;
using Foodie.Controllers;

namespace Foodie.Controllers
{
    public class TasteBudsController : Controller
    {
        private FoodieEntities db = new FoodieEntities();

        public object FoodieModel { get; private set; }

        // GET: TasteBuds
        public ActionResult Index(string countryName, string searchString)
        {
            var CountryList = new List<string>();


                var CountryQry = from cname in db.TasteBuds
                                 orderby cname.Country
                                 select cname.Country;

            CountryList.AddRange(CountryQry.Distinct());
            ViewBag.countryName = new SelectList(CountryList);

            var country = from c in db.TasteBuds
                          select c;

            if(!String.IsNullOrEmpty(searchString))
            {
                country = country.Where(a => a.FoodName.Contains(searchString));
            }
            if(!String.IsNullOrEmpty(countryName))
            {
                country = country.Where(x => x.Country == countryName);
            }

             return View(country);
        }

        // GET: TasteBuds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasteBud tasteBud = db.TasteBuds.Find(id);
            if (tasteBud == null)
            {
                return HttpNotFound();
            }
            return View(tasteBud);
        }

        // GET: TasteBuds/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: TasteBuds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodId,FoodName,Country,Info,Image")] TasteBud tasteBud)
        {
            if (ModelState.IsValid)
            {
                db.TasteBuds.Add(tasteBud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tasteBud);
        }

        // GET: TasteBuds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasteBud tasteBud = db.TasteBuds.Find(id);
            if (tasteBud == null)
            {
                return HttpNotFound();
            }
            return View(tasteBud);
        }

        // POST: TasteBuds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FoodId,FoodName,Country,Info,Image")] TasteBud tasteBud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tasteBud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasteBud);
        }

        // GET: TasteBuds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TasteBud tasteBud = db.TasteBuds.Find(id);
            if (tasteBud == null)
            {
                return HttpNotFound();
            }
            return View(tasteBud);
        }

        // POST: TasteBuds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TasteBud tasteBud = db.TasteBuds.Find(id);
            db.TasteBuds.Remove(tasteBud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
