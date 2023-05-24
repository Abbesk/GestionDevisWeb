using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using LogicomDevisBackEnd.Models;

namespace LogicomDevisBackEnd.Controllers
{
    public class LigneDepotController : ApiController
    {
        private static string societyName = (string)HttpContext.Current.Cache["SelectedSoc"] ;
        private string connectionString;
        private SocieteEntities db;

        public LigneDepotController()
        {
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["SocieteEntities"].ConnectionString, societyName);
            db = new SocieteEntities(connectionString);
        }

        [Authorize]
        public IQueryable<lignedepot> Getlignedepot()
        {
            return db.lignedepot;
        }

        [Authorize]
        [ResponseType(typeof(lignedepot))]
        public IHttpActionResult Getlignedepot(string id)
        {
            lignedepot lignedepot = db.lignedepot.Find(id);
            if (lignedepot == null)
            {
                return NotFound();
            }

            return Ok(lignedepot);
        }
        [Authorize]
        [HttpGet]
        [Route("api/LigneDepot/FetchLigneDepotParDepEtPv")]
        public async Task<IEnumerable<lignedepot>> FetchLigneDepotParDepEtPv(string codedep ,string codepv)
        {
            
            var lstlignes = await db.lignedepot.ToListAsync();
           
            if (codedep != null && codepv!=null)
            {
                lstlignes = lstlignes.Where(f => f.codedep == codedep).ToList();
                lstlignes = lstlignes.Where(f => f.codepv == codepv).ToList();
                
            }
            return lstlignes;
        }
        [Authorize]
        [HttpGet]
        [Route("api/LigneDepot/VerifierQuantite")]
        public async Task<double> VerifierQuantite(string codedep, string codepv,string codeArt , string fam )
        {

            var lstlignes = await db.lignedepot.ToListAsync();

            if (codedep != null && codepv != null && codeArt!=null && fam!=null) 
            {
                lstlignes = lstlignes.Where(f => f.famille == fam).ToList();
                lstlignes = lstlignes.Where(f => f.codeart == codeArt).ToList();
                lstlignes = lstlignes.Where(f => f.codedep == codedep).ToList();
                lstlignes = lstlignes.Where(f => f.codepv == codepv).ToList();
                if (lstlignes.Count() == 1 && lstlignes[0].qteart != null)
                {
                    return ((double)lstlignes[0].qteart);
                }
                else
                    return 0; 
                 
            }
            return 0; 
        }
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlignedepot(string id, lignedepot lignedepot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lignedepot.codedep)
            {
                return BadRequest();
            }

            db.Entry(lignedepot).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!lignedepotExists(id))
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
        [ResponseType(typeof(lignedepot))]
        public IHttpActionResult Postlignedepot(lignedepot lignedepot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.lignedepot.Add(lignedepot);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (lignedepotExists(lignedepot.codedep))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lignedepot.codedep }, lignedepot);
        }

        [Authorize]
        [ResponseType(typeof(lignedepot))]
        public IHttpActionResult Deletelignedepot(string id)
        {
            lignedepot lignedepot = db.lignedepot.Find(id);
            if (lignedepot == null)
            {
                return NotFound();
            }

            db.lignedepot.Remove(lignedepot);
            db.SaveChanges();

            return Ok(lignedepot);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool lignedepotExists(string id)
        {
            return db.lignedepot.Count(e => e.codedep == id) > 0;
        }
    }
}