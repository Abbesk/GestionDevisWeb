using System;
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
    public class DevisController : ApiController
    {
        private  string societyName = (string)HttpContext.Current.Cache["SelectedSoc"] ;
        private string connectionString;
        private SocieteEntities db;

        public DevisController()
        {
            connectionString = string.Format(ConfigurationManager.ConnectionStrings["SocieteEntities"].ConnectionString, societyName);
            db = new SocieteEntities(connectionString);
        }
        // GET: api/Devis

        [Authorize]
        public async Task<IEnumerable<dfp>> GetAllDevis()
            {
            
                return await db.dfp.ToListAsync();

            }
        [Authorize]
        [HttpGet]
        [Route("api/Devis/NouveauIndex/")]
        public Task<string> NouveauIndex()
        {
            if (db.dfp.Count() == 0)
                
            return Task.FromResult("DV"+DateTime.Now.ToString("yy")+"00001");
            else
            {
                List<dfp> dfps = db.dfp.ToList();
                
                string dernierAnnée = dfps.LastOrDefault().NUMBL.Substring(2, 2); 
                if(dernierAnnée== DateTime.Now.ToString("yy"))
                {
                    string dernierNumero = (Convert.ToInt32(dfps.LastOrDefault().NUMBL.Substring(4, 5))+1).ToString();
                    if (dernierNumero.Length == 1)
                    {
                        return Task.FromResult("DV" + DateTime.Now.ToString("yy") + "0000" + dernierNumero);
                    }
                    else
                    {
                        if (dernierNumero.Length ==2)
                        {
                            return Task.FromResult("DV" + DateTime.Now.ToString("yy") + "000" + dernierNumero);
                        }
                        else
                        {
                            if (dernierNumero.Length == 3)
                            {
                                return Task.FromResult("DV" + DateTime.Now.ToString("yy") + "00" + dernierNumero);
                            }
                            else
                            {
                                if (dernierNumero.Length == 4)
                                {
                                    return Task.FromResult("DV" + DateTime.Now.ToString("yy") + "0" + dernierNumero);
                                }
                                else
                                {
                                    return Task.FromResult("DV" + DateTime.Now.ToString("yy")  + dernierNumero);
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    return Task.FromResult("DV" + DateTime.Now.ToString("yy") + "00001");
                }                          

            }

        }
        [Authorize]
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Devis/SelectionnerArticles")]
        public IHttpActionResult Selectionner (string id ,dfp devis)
        {
            dfp ExistingDevis = new dfp(); 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != devis.NUMBL)
            {
                return BadRequest();
            }
            if(id!= null && devis != null)
            {
            ExistingDevis = db.dfp.Find(id); 
            if(ExistingDevis!=null)
                {
                    if (ExistingDevis.LignesDevis.Count() != 0){
                        foreach(ldfp l in ExistingDevis.LignesDevis.ToList())
                        {
                            ExistingDevis.LignesDevis.Remove(l); 
                        }
                    }
                    foreach(ldfp ligne in devis.LignesDevis)
                    {
                        if(ligne != null && ligne.QteART!= 0 && ligne.codedep != null && ligne.famille != null && ligne.CodeART !=null)
                        {
                            ligne.DateBL = ExistingDevis.DATEBL; 
                            ExistingDevis.LignesDevis.Add(ligne);
                        }
                        
                    }
                    ExistingDevis.MHT = devis.MHT;
                    ExistingDevis.TAUXREMISE = devis.TAUXREMISE;
                    ExistingDevis.MTTC = devis.MTTC;
                    ExistingDevis.DATEDMAJ = DateTime.Now; 
                }
            }

            db.Entry(ExistingDevis).State = EntityState.Modified; 


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dfpExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtRoute("DefaultApi", new { id = devis.NUMBL }, devis);
        }

        [Authorize]
        [ResponseType(typeof(dfp))]
        public async Task<IHttpActionResult> GetDevis(string id)
        {
            dfp devis = await db.dfp.SingleAsync(f => f.NUMBL == id);
          
            db.Entry(devis).Collection(f => f.LignesDevis)
                                       .Query()
                                       .Include(f => f.Article)
                                       .Where(d=>d.CodeART==d.Article.code)
                                       .Load();
           
            if (devis == null)
            {
                return NotFound();
            }

            return Ok(devis);
        }

        // PUT: api/Devis/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDevis(string id, dfp dfp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dfp.NUMBL)
            {
                return BadRequest();
            }

            db.Entry(dfp).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!dfpExists(id))
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
        [ResponseType(typeof(dfp))]
        public IHttpActionResult PostDevis(dfp devis)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (devis.codepv != null)
            {
                devis.PointVente = db.pointvente.Find(devis.codepv);
                devis.libpv = devis.PointVente.Libelle; 
            }
            if (devis.CODECLI != null )
            {
                devis.Client = db.client.Find(devis.CODECLI);
                devis.ADRCLI = devis.Client.adresse;
                devis.RSCLI = devis.Client.rsoc;
                devis.confcl = devis.Client.rsoc;
            }
            devis.CODEFACTURE = "N";
            devis.etatcl = "";
            devis.subv = "0";
            devis.cred = "0";
            devis.DATEBL = DateTime.Now;
            devis.datelimit = DateTime.Now.AddDays(15); 
            foreach(ldfp ligne in devis.LignesDevis)
            {
                ligne.NumBL = devis.NUMBL;
                db.ldfp.Add(ligne);
            }
            db.dfp.Add(devis);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (dfpExists(devis.NUMBL))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = devis.NUMBL }, devis);
        }

        [Authorize]
        [ResponseType(typeof(dfp))]
        public IHttpActionResult DeleteDevis(string id)
        {
            dfp dfp = db.dfp.Find(id);
            IQueryable<ldfp> ldfps = db.ldfp.Where(ligne => ligne.NumBL == id);

            if (dfp == null)
            {
                return NotFound();
            }

            
            foreach (var ldfp in ldfps)
            {
                db.ldfp.Remove(ldfp);
            }

            
            db.dfp.Remove(dfp);
            db.SaveChanges();

            return Ok(dfp);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool dfpExists(string id)
        {
            return db.dfp.Count(e => e.NUMBL == id) > 0;
        }
    }
}