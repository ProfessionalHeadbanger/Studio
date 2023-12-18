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

namespace Studio.Controllers
{
    public class PrinterController : Controller
    {
        private readonly ControlClass<Printer> cc;

        public PrinterController()
        {
            cc = new ControlClass<Printer>(new StudioContext());
        }

        // GET: Printer
        public ActionResult Index()
        {
            return View(cc.GetAll());
        }

        // GET: Printer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = cc.GetById(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // GET: Printer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Printer/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Manufacturer,Name,Status")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                cc.Insert(printer);
                cc.Save();
                return RedirectToAction("Index");
            }

            return View(printer);
        }

        // GET: Printer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = cc.GetById(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: Printer/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Manufacturer,Name,Status")] Printer printer)
        {
            if (ModelState.IsValid)
            {
                cc.Update(printer);
                cc.Save();
                return RedirectToAction("Index");
            }
            return View(printer);
        }

        // GET: Printer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Printer printer = cc.GetById(id);
            if (printer == null)
            {
                return HttpNotFound();
            }
            return View(printer);
        }

        // POST: Printer/Delete/5
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
