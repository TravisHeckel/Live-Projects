using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;

namespace TheatreCMS3.Areas.Rent.Controllers
{
    public class RentalsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/Rentals
        public ActionResult Index()
        {
            return View(db.Rentals.ToList());
        }

        // GET: Rent/Rentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // GET: Rent/Rentals/Create
        public ActionResult Create()
        {
            var RentalTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select an Option", Value = "Select an Option" },
                new SelectListItem { Text = "Rental", Value = "Rental" },
                new SelectListItem { Text = "RentalEquipment", Value = "RentalEquipment" },
                new SelectListItem { Text = "RentalRoom", Value = "RentalRoom" }
            };

            ViewBag.RentalTypes = new SelectList(RentalTypes, "Text", "Value");

            return View();
        }

        // POST: Rent/Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

  
        public ActionResult Create(Rental rental)
        {
            
            //this saves it to the database 
            if (ModelState.IsValid)
            {
                db.Rentals.Add(rental);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(rental);
        }

        // GET: Rent/Rentals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            var RentalTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select an Option", Value = "Select an Option" },
                new SelectListItem { Text = "Rental", Value = "Rental" },
                new SelectListItem { Text = "RentalEquipment", Value = "RentalEquipment" },
                new SelectListItem { Text = "RentalRoom", Value = "RentalRoom" }
            };

            ViewBag.RentalTypes = new SelectList(RentalTypes, "Text", "Value");

            return View(rental);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Rental modifiedRental)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing entity from the database
                var existingRental = db.Rentals.Find(modifiedRental.RentalId);

                if (existingRental == null)
                {
                    return HttpNotFound();
                }

                // Update the properties of the existing entity with the modified values
                existingRental.RentalName = modifiedRental.RentalName;
                existingRental.RentalCost = modifiedRental.RentalCost;
                existingRental.FlawsAndDamages = modifiedRental.FlawsAndDamages;
                existingRental.RentalType = modifiedRental.RentalType;

                // Depending on the RentalType, update the related properties

                if (modifiedRental.RentalType == "Rental")
                {
                    existingRental.RentalEquipment.ChokingHazard = null;
                    existingRental.RentalEquipment.SuffocationHazard = null;
                    existingRental.RentalEquipment.PurchasingPrice = null;
                    existingRental.RentalRoom.MaxOccupancy = null;
                    existingRental.RentalRoom.RoomNumber = null;
                    existingRental.RentalRoom.SquareFootage = null;
                }
                else if (modifiedRental.RentalType == "RentalEquipment")
                {
                    existingRental.RentalEquipment.ChokingHazard = modifiedRental.RentalEquipment.ChokingHazard;
                    existingRental.RentalEquipment.SuffocationHazard = modifiedRental.RentalEquipment.SuffocationHazard;
                    existingRental.RentalEquipment.PurchasingPrice = modifiedRental.RentalEquipment.PurchasingPrice;
                    existingRental.RentalRoom.MaxOccupancy = null;
                    existingRental.RentalRoom.RoomNumber = null;
                    existingRental.RentalRoom.SquareFootage = null;
                }
                else if (modifiedRental.RentalType == "RentalRoom")
                {
                    existingRental.RentalEquipment.ChokingHazard = null;
                    existingRental.RentalEquipment.SuffocationHazard = null;
                    existingRental.RentalEquipment.PurchasingPrice = null;
                    existingRental.RentalRoom.MaxOccupancy = modifiedRental.RentalRoom.MaxOccupancy;
                    existingRental.RentalRoom.RoomNumber = modifiedRental.RentalRoom.RoomNumber;
                    existingRental.RentalRoom.SquareFootage = modifiedRental.RentalRoom.SquareFootage;
                }

                // Set the entity state to Modified
                db.Entry(existingRental).State = EntityState.Modified;

                // Save changes to the database
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(modifiedRental);
        }


        // GET: Rent/Rentals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return HttpNotFound();
            }
            return View(rental);
        }

        // POST: Rent/Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rental rental = db.Rentals.Find(id);
            db.Rentals.Remove(rental);
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
