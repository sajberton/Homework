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
    public class MatchesController : Controller
    {
        private WorldCupDbContext db = new WorldCupDbContext();

        // GET: Matches
        public ActionResult Index()
        {
            //var match = db.Matches.Include(x => x.Team1).Include(x => x.Team2).First();
            //ViewBag.NationalTeams = new SelectList(db.NationalTeams, "NationalTeamId", "Name");
            return View(db.Matches.Include(x => x.Team1).Include(x => x.Team2).ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Include(x => x.Team1).Include(x => x.Team2).FirstOrDefault(x => x.MatchId == id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            ViewBag.NationalTeams = db.NationalTeams.ToList();
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchId,MatchDateTime,Information,Team1Id,Team2Id")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.Team1 = db.NationalTeams.Find(match.Team1Id);
                match.Team2 = db.NationalTeams.Find(match.Team2Id);
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Match match = db.Matches.Include(x=> x.Team1).Include(x => x.Team2).FirstOrDefault(x => x.MatchId==id);
        
            if (match == null)
            {
                return HttpNotFound();
            }

            match.Team1Id = match.Team1.NationalTeamId;
            match.Team2Id = match.Team2.NationalTeamId;
            ViewBag.NationalTeams = db.NationalTeams.ToList();
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchId,MatchDateTime,Information,Team1Id,Team2Id")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();

                var matchFromDb = db.Matches.Include(x => x.Team1).Include(x => x.Team2).First(x => x.MatchId == match.MatchId);
                matchFromDb.Team1 = db.NationalTeams.FirstOrDefault(x => x.NationalTeamId == match.Team1Id);
                matchFromDb.Team2 = db.NationalTeams.FirstOrDefault(x => x.NationalTeamId == match.Team2Id);
                db.SaveChanges();

                //var dbMatch1 = db.Matches.Include(x => x.Team1).FirstOrDefault(x => x.MatchId == match.MatchId);
                //if (dbMatch1 != null &&
                //    dbMatch1.Team1.NationalTeamId != match.Team1Id)
                //{
                //    //var oldTeam1 = db.NationalTeams.FirstOrDefault(x => x.NationalTeamId == dbMatch1.Team1.NationalTeamId);

                //    var newTeam1 = db.NationalTeams.FirstOrDefault(x => x.NationalTeamId == match.Team1.NationalTeamId);

                //    match.Team1 = newTeam1;
                //    db.SaveChanges();

                //}
                
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
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
