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
    public class ArticleController : ApiController
    {
        
        private somabeEntities db = new somabeEntities();

        [Authorize]
        public IQueryable<article> Getarticle()
        {
            return db.article;
        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Article/FetchCodeArtEtFamille")]
        public async Task<IEnumerable<article>> FetchLigneDepotParDepEtPv(string codeart, string fam)
        {

            var lstArticles = await db.article.ToListAsync();

            if (codeart != null && fam != null)
            {
                lstArticles = lstArticles.Where(f => f.code == codeart).ToList();
                lstArticles = lstArticles.Where(f => f.fam == fam).ToList();

            }
            return lstArticles;
        }
        [Authorize]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Article/GetRemiseArticle")]
        public async Task<double> VerifierRemiseArticle (string codeart, string fam)
        {

            var lstArticles = await db.article.ToListAsync();

            if (codeart != null && fam != null)
            {
                lstArticles = lstArticles.Where(f => f.code == codeart).ToList();
                lstArticles = lstArticles.Where(f => f.fam == fam).ToList();
            if (lstArticles.Count() > 0 && lstArticles[0].DREMISE!=null)
            {
                    return (double)lstArticles[0].DREMISE;
            }
                else
                {
                    return 0; 
                }
            }
            return 0;
        }

        [Authorize]
        [ResponseType(typeof(article))]
        public IHttpActionResult Getarticle(string id)
        {
            article article = db.article.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            return Ok(article);
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Putarticle(string id, article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != article.code)
            {
                return BadRequest();
            }

            db.Entry(article).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!articleExists(id))
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
        [ResponseType(typeof(article))]
        public IHttpActionResult Postarticle(article article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.article.Add(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (articleExists(article.code))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = article.code }, article);
        }

        [Authorize]
        [ResponseType(typeof(article))]
        public IHttpActionResult Deletearticle(string id)
        {
            article article = db.article.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            db.article.Remove(article);
            db.SaveChanges();

            return Ok(article);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool articleExists(string id)
        {
            return db.article.Count(e => e.code == id) > 0;
        }
    }
}