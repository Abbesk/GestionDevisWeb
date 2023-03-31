using LogicomDevisFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LogicomDevisFrontEnd.Controllers
{
    public class DevisController : Controller
    {


        string baseURL = "http://localhost:44333/Api/";

        
        // GET: Devis
        public async Task<ActionResult> Index()
        {

            IList<Devis> lstdevis = new List<Devis>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL );
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Devis");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    lstdevis = JsonConvert.DeserializeObject<List<Devis>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewData.Model = lstdevis;

            }
            /////Charger point de vente 
            IList<PointVente> pointVentes = new List<PointVente>(); 
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];                
                client.BaseAddress = new Uri(baseURL );
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("PointVente");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    pointVentes = JsonConvert.DeserializeObject<List<PointVente>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewBag.pvs = pointVentes;

            }


            return View();
        }

       
        public async Task<ActionResult> SelectionnerArticles(string id, Devis model)
        {
            IList<Depot> depots = new List<Depot>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                
                client.BaseAddress = new Uri("http://localhost:44333/api/Depot");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Depot");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    depots = JsonConvert.DeserializeObject<List<Depot>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }

                ViewBag.codeDepot = new SelectList(depots, "code", "code");
            }
            if (model.NUMBL != null)
            {
                using (HttpClient client = new HttpClient())
                {
                    string token = (string)Session["token"];                    
                    client.BaseAddress = new Uri("http://localhost:44333/api/Devis/SelectionnerArticles?id=" + id);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    string jsonContent = JsonConvert.SerializeObject(model);
                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(client.BaseAddress, content).ConfigureAwait(false);



                    return RedirectToAction("Index");


                }

            }
            /////////
            IList<Devis> lstDevis = new List<Devis>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Devis");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    lstDevis = JsonConvert.DeserializeObject<List<Devis>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }

            }
            ////////
            Devis d = new Devis();
            foreach (Devis dev in lstDevis)
            {
                if (dev.NUMBL == id)
                {
                    d = dev;
                    break;

                }
            }
            d.LignesDevis.Add(new LigneDevis() { NumBL = d.NUMBL });
            return View(d);
        }

        public async Task<ActionResult> Create(Devis devis)
        {
            string nouveauIndex="";

            IList<PointVente> pointVentes = new List<PointVente>();

            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                
                client.BaseAddress = new Uri(baseURL+"Devis/NouveauIndex");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync(client.BaseAddress);
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    nouveauIndex = JsonConvert.DeserializeObject<String>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }

                
            }
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("PointVente");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    pointVentes = JsonConvert.DeserializeObject<List<PointVente>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                
                ViewBag.pvs = new SelectList(pointVentes, "code", "libelle");

            }











            if (devis.NUMBL != null)
            {



                using (var client = new HttpClient())
                {
                    string token = (string)Session["token"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(devis);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseURL + "Inventaire");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage getData = await client.PostAsync("Devis", data);
                    if (getData.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index");

                    }
                    else
                    {
                       
                        return View("ErreurCreation");
                    }
                }

            }


            devis = new Devis
            {
                NUMBL = nouveauIndex,
                CODEFACTURE = "N",

                DATEBL = DateTime.Now,
                mlettre = " Devis En Cours -- crée le :" + DateTime.Now.ToShortDateString() + "/ par" + ": " + devis.usera + ".",
                commentaire = "Espérons que notre offre trouve votre entière satisfaction.Veuillez agréer, Monsieur(Madame),nos sentiments les plus distingués.",
                commentete = "Cher Monsieur; Suite à votre demande nous avons le plaisir de vous communiquer notre meilleur offre de prix pour:",
                valorisation = "HT"

            };
            ViewBag.nouveau = nouveauIndex;
            return View(devis);
        }
    }
}