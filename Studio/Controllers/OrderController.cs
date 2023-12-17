using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Studio.DAL;
using Studio.Models;
using static System.Collections.Specialized.BitVector32;

namespace Studio.Controllers
{
    public class OrderController : Controller
    {
        private readonly GenericRepository<Order> rep;
        private readonly GenericRepository<Customer> customerRep;
        private readonly GenericRepository<Model> modelRep;
        private readonly GenericRepository<Printer> printerRep;

        public OrderController()
        {
            rep = new GenericRepository<Order>(new StudioContext());
            customerRep = new GenericRepository<Customer>(new StudioContext());
            modelRep = new GenericRepository<Model>(new StudioContext());
            printerRep = new GenericRepository<Printer>(new StudioContext());
        }

        // GET: Order
        public ActionResult Index()
        {
            var orders = rep.GetAll().Include(o => o.Customer).Include(o => o.Model).Include(o => o.Printer).ToList();
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = rep.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(customerRep.GetAll(), "Id", "Name");
            ViewBag.ModelId = new SelectList(modelRep.GetAll(), "Id", "Name");
            ViewBag.PrinterId = new SelectList(printerRep.GetAll(), "Id", "Manufacturer");
            return View();
        }

        // POST: Order/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,Price,CustomerId,ModelId,PrinterId")] Order order)
        {
            if (ModelState.IsValid)
            {
                rep.Insert(order);
                rep.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(customerRep.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelRep.GetAll(), "Id", "Name", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerRep.GetAll(), "Id", "Manufacturer", order.PrinterId);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = rep.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(customerRep.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelRep.GetAll(), "Id", "Name", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerRep.GetAll(), "Id", "Manufacturer", order.PrinterId);
            return View(order);
        }

        // POST: Order/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,Price,CustomerId,ModelId,PrinterId")] Order order)
        {
            if (ModelState.IsValid)
            {
                rep.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(customerRep.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelRep.GetAll(), "Id", "Name", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerRep.GetAll(), "Id", "Manufacturer", order.PrinterId);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = rep.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            rep.Delete(id);
            rep.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
