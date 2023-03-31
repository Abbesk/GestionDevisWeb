using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LogicomDevisBackEnd.Models;

namespace LogicomDevisBackEnd.Controllers
{
    public class DepotController : ApiController
    {
        private somabeEntities db = new somabeEntities();


        [Authorize]
        // GET: api/Depot
        public async Task<IEnumerable<depot>> Getdepot()
        {
            return await db.depot.ToListAsync();
        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Depot/DepotParCodePV")]
        public async Task<IEnumerable<depot>> DepotParCodePV(string code)
        {
            Console.WriteLine("Code parameter: " + code);
            var depots = await db.depot.ToListAsync();
            Console.WriteLine("Total number of depots: " + depots.Count);
            if (code != null)
            {
                depots = depots.Where(f => f.codepv == code).ToList();
                Console.WriteLine("Number of depots with matching codepv: " + depots.Count);
            }
            return depots;
        }

        [Authorize]
        [ResponseType(typeof(depot))]
        public IHttpActionResult Getdepot(string id)
        {
            depot depot = db.depot.Find(id);
            if (depot == null)
            {
                return NotFound();
            }

            return Ok(depot);
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putdepot(string id, depot depot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != depot.Code)
            {
                return BadRequest();
            }

            db.Entry(depot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!depotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        [ResponseType(typeof(depot))]
        public IHttpActionResult Postdepot(depot depot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.depot.Add(depot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (depotExists(depot.Code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = depot.Code }, depot);
        }

        [Authorize]

        [ResponseType(typeof(depot))]
        public IHttpActionResult Deletedepot(string id)
        {
            depot depot = db.depot.Find(id);
            if (depot == null)
            {
                return NotFound();
            }

            db.depot.Remove(depot);
            db.SaveChanges();

            return Ok(depot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool depotExists(string id)
        {
            return db.depot.Count(e => e.Code == id) > 0;
        }
    }
}