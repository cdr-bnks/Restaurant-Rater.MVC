using RestaurantRater.MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantRater.MVC.Controllers
{
    public class RestaurantController : Controller
    {
        private RestaurantDbContext _context = new RestaurantDbContext();
        // GET: Restaurant/Index
        public ActionResult Index()
        {
            return View(_context.Restaurants.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        //POST: Restaurant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(restaurant);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(restaurant);
            }
        }

        //GET: Restaurant/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Restaurant restaurant = _context.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        //POST: Restaurant/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Restaurant restaurant = _context.Restaurants.Find(id);
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
            // Just try Redirect next time
            return RedirectToAction("Index");
        }
        //GET: Restaurant/Edit/{id}
        
        public ActionResult Edit(int? id)
        { // Get an id from the user

            if (id == null)
            { // Handle if the id is null
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Find Restaurant by that id
            Restaurant restaurant = _context.Restaurants.Find(id);

            if (restaurant == null)
            {// If the Restaurant doesn't exist
                return HttpNotFound();
            }
            else
            { // Return the restaurant and the view
                return View(restaurant);
            }
        }

        //POST: Restaurant/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Restaurant restaurant)
        { // Get restaurant

            // If restaurant has any errors handle that
            if (ModelState.IsValid)
            {// Find the Entry(Method) model in the DB, access its State(Prop) and let user know it is modified
                _context.Entry(restaurant).State = EntityState.Modified;
                // Find any row that has been modified and update it
                _context.SaveChanges();
                // Once this action is completed return to the Index
                return RedirectToAction("Index");
            }
            else
            {
                return View(restaurant);
            }
        }

        //GET: Restaurant/Details/{id}
        public ActionResult Details (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = _context.Restaurants.Find(id);
            
            if(restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }
    }
}