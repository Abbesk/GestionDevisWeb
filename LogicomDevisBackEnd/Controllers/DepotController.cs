using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using LogicomDevisBackEnd.Models;

namespace LogicomDevisBackEnd.Controllers
{
    public class DepotController : ApiController
    {
        private static string societyName = (string)HttpContext.Current.Cache["SelectedSoc"] ;
        private string connectionString;
        private SocieteEntities db;

        public DepotController()
        {
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["SocieteEntities"].ConnectionString, societyName);
            db = new SocieteEntities(connectionString);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Depot/GetDepotsParUser")]
        public async Task<IEnumerable<depot>> GetDepotsParUser(string codeuser)
        {


            List<utilisateurpv> UsersPV = db.utilisateurpv.Where(pv => pv.codeuser == codeuser).ToList();
            var codePvList = UsersPV.Select(pv => pv.codepv).ToList();

            return await db.depot.Where(dep => codePvList.Contains(dep.codepv)).ToListAsync();

        }

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
            var depots = await db.depot.ToListAsync();
            if (code != null)
            {
                depots = depots.Where(f => f.codepv == code).ToList();
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