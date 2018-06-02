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
    public class PlayersController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: Players
        public ActionResult Index()
        {
            return View(db.Players.Include(x => x.Team).ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Include(x => x.Team).FirstOrDefault(z => z.PlayerId == id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.NationalTeams = db.NationalTeams.ToList();
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerId,Name,Position,Info,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                player.Team = db.NationalTeams.Find(player.TeamId);
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.NationalTeams = db.NationalTeams.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Include(x => x.Team).FirstOrDefault(z => z.PlayerId == id);
            if (player == null)
            {
                return HttpNotFound();
            }

            player.TeamId = player.Team.NationalTeamId;
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,Name,Position,Info,TeamId")] Player player)
        {
            ViewBag.NationalTeams = db.NationalTeams.ToList();
            if (ModelState.IsValid)
            {
                //player.Team = db.NationalTeams.Find(player.TeamId);
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();

                var playerFromDb = db.Players.Include(x => x.Team).FirstOrDefault(z => z.PlayerId == player.PlayerId);
                playerFromDb.Team = db.NationalTeams.FirstOrDefault(x => x.NationalTeamId == player.TeamId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
