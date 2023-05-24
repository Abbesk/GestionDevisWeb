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
    public class ClientController : ApiController
    {
        private static string societyName = (string)HttpContext.Current.Cache["SelectedSoc"] ;
        private string connectionString;
        private SocieteEntities db;

        public ClientController()
        {
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["SocieteEntities"].ConnectionString, societyName);
            db = new SocieteEntities(connectionString);
        }

        [Authorize]
        public async Task <List<client>> Getclient()
        {
            return await db.client.ToListAsync();
        }
        [Authorize]
        [HttpGet]
        [Route("api/Client/NouveauIndex/")]
        public Task<string> NouveauIndex()
        {
            if (db.client.Count() == 0)
            {
                return Task.FromResult("00000001"); 
                
            }
            else
            {
                List<client> clients = db.client.ToList(); 
                string nv ="";                
                string dernierNumero = (Convert.ToInt32(clients.LastOrDefault().code) + 1).ToString();
                int i = dernierNumero.Length;
                for (int j =0; j<8-i; j++)
                {
                    nv = nv + "0";
                }
                return Task.FromResult(nv + dernierNumero);  
            }
        }
            [Authorize]
        [ResponseType(typeof(client))]
        public IHttpActionResult Getclient(string id)
        {
            client client = db.client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Client/GetclientByRsoc")]
        public async Task<IHttpActionResult> GetclientByRsoc(string rsoc)
        {
            var client = await db.client.FirstOrDefaultAsync(cl => cl.rsoc == rsoc);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Client/GetClientByRS")]
        public Task<string> GetClientByRS(string rs)
        {
            if (rs != null)
            {
                List<client> clients = db.client.ToList();
                foreach (client client in clients)
                    if (client.rsoc == rs)
                    {
                        return Task.FromResult(client.code);
                    }
            }
            return Task.FromResult("");
        }
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putclient(string id, client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.code)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!clientExists(id))
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
        [ResponseType(typeof(client))]
        public IHttpActionResult Postclient(client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            client.reference = "0";
            client.codesel = "0"; 
            db.client.Add(client);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (clientExists(client.code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = client.code }, client);
        }

        // DELETE: api/Client/5
        [ResponseType(typeof(client))]
        public IHttpActionResult Deleteclient(string id)
        {
            client client = db.client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool clientExists(string id)
        {
            return db.client.Count(e => e.code == id) > 0;
        }
    }
}