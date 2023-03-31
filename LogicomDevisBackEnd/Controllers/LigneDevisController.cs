using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LogicomDevisBackEnd.Models;

namespace LogicomDevisBackEnd.Controllers
{
    public class LigneDevisController : ApiController
    {
        private somabeEntities db = new somabeEntities();

        [Authorize]
        public IQueryable<ldfp> Getldfp()
        {
            return db.ldfp;
        }

        [Authorize]
        [ResponseType(typeof(ldfp))]
        public IHttpActionResult Getldfp(string id)
        {
            ldfp ldfp = db.ldfp.Find(id);
            if (ldfp == null)
            {
                return NotFound();
            }

            return Ok(ldfp);
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putldfp(string id, ldfp ldfp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ldfp.NumBL)
            {
                return BadRequest();
            }

            db.Entry(ldfp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ldfpExists(id))
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
        [ResponseType(typeof(ldfp))]
        public IHttpActionResult Postldfp(ldfp ldfp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ldfp.Add(ldfp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ldfpExists(ldfp.NumBL))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ldfp.NumBL }, ldfp);
        }

        [Authorize]
        [ResponseType(typeof(ldfp))]
        public IHttpActionResult Deleteldfp(string id)
        {
            ldfp ldfp = db.ldfp.Find(id);
            if (ldfp == null)
            {
                return NotFound();
            }

            db.ldfp.Remove(ldfp);
            db.SaveChanges();

            return Ok(ldfp);
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize]
        private bool ldfpExists(string id)
        {
            return db.ldfp.Count(e => e.NumBL == id) > 0;
        }
    }
}