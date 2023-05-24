using LogicomDevisBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LogicomDevisBackEnd.Controllers
{
    public class RepresentantController : ApiController
    {
        private static string societyName = (string)HttpContext.Current.Cache["SelectedSoc"];
        private string connectionString;
        private SocieteEntities db;

        public RepresentantController()
        {
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["SocieteEntities"].ConnectionString, societyName);
            db = new SocieteEntities(connectionString);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Representant/GetRepresentants")]
        public async Task<IEnumerable<representant>> GetRepresentants()
        {
            return await db.representant.ToListAsync();

        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Representant/GetRepresentantByRs")]
        public async Task<IHttpActionResult> GetRepresentantByRs(string rsoc)
        {
            var representant = await db.representant.FirstOrDefaultAsync(cl => cl.rsoc == rsoc);

            if (representant == null)
            {
                return NotFound();
            }

            return Ok(representant);
        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Representant/GetRepresentantById")]
        public async Task<IHttpActionResult> GetRepresentantById(string id)
        {
            var representant = await db.representant.FirstOrDefaultAsync(cl => cl.Code == id);

            if (representant == null)
            {
                return NotFound();
            }

            return Ok(representant);
        }

    }
}
