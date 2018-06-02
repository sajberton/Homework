using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldCup.Models;

namespace WorldCup.Controllers
{
    public class NationalTeamsController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: NationalTeams
        public ActionResult Index()
        {
            return View(db.NationalTeams.ToList());
        }

        // GET: NationalTeams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalTeam nationalTeam = db.NationalTeams.Find(id);
            if (nationalTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationalTeam);
        }

        // GET: NationalTeams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NationalTeams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NationalTeamId,Name,Continent,Information,Players")] NationalTeam nationalTeam)
        {
            if (ModelState.IsValid)
            {
                db.NationalTeams.Add(nationalTeam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nationalTeam);
        }

        // GET: NationalTeams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalTeam nationalTeam = db.NationalTeams.Find(id);
            if (nationalTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationalTeam);
        }

        // POST: NationalTeams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NationalTeamId,Name,Continent,Information,Players")] NationalTeam nationalTeam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nationalTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nationalTeam);
        }

        // GET: NationalTeams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NationalTeam nationalTeam = db.NationalTeams.Find(id);
            if (nationalTeam == null)
            {
                return HttpNotFound();
            }
            return View(nationalTeam);
        }

        // POST: NationalTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NationalTeam nationalTeam = db.NationalTeams.Find(id);
            db.NationalTeams.Remove(nationalTeam);
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
