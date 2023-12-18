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
        private readonly ControlClass<Order> cc;
        private readonly ControlClass<Customer> customerCc;
        private readonly ControlClass<Model> modelCc;
        private readonly ControlClass<Printer> printerCc;

        public OrderController()
        {
            cc = new ControlClass<Order>(new StudioContext());
            customerCc = new ControlClass<Customer>(new StudioContext());
            modelCc = new ControlClass<Model>(new StudioContext());
            printerCc = new ControlClass<Printer>(new StudioContext());
        }

        // GET: Order
        public ActionResult Index()
        {
            var orders = cc.GetAll().Include(o => o.Customer).Include(o => o.Model).Include(o => o.Printer).ToList();
            return View(orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = cc.GetAll().Include(o => o.Customer).Include(o => o.Model).Include(o => o.Printer).Include(o => o.Materials).SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(customerCc.GetAll(), "Id", "Name");
            ViewBag.ModelId = new SelectList(modelCc.GetAll(), "Id", "ModelName");
            ViewBag.PrinterId = new SelectList(printerCc.GetAll(), "Id", "Manufacturer");
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
                cc.Insert(order);
                cc.Save();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(customerCc.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelCc.GetAll(), "Id", "ModelName", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerCc.GetAll(), "Id", "Manufacturer", order.PrinterId);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = cc.GetById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(customerCc.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelCc.GetAll(), "Id", "ModelName", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerCc.GetAll(), "Id", "Manufacturer", order.PrinterId);
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
                cc.Update(order);
                cc.Save();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(customerCc.GetAll(), "Id", "Name", order.CustomerId);
            ViewBag.ModelId = new SelectList(modelCc.GetAll(), "Id", "ModelName", order.ModelId);
            ViewBag.PrinterId = new SelectList(printerCc.GetAll(), "Id", "Manufacturer", order.PrinterId);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = cc.GetById(id);
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
            cc.Delete(id);
            cc.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                cc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
