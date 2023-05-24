using LogicomDevisFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LogicomDevisFrontEnd.Controllers
{
    public class DevisController : Controller
    {


        string baseURL = "http://localhost:44333/Api/";

        public async Task<ActionResult> CreateClient(Client c)
        {

            if (c.code== null)
            {
                string nouveauIndex;
                using (var client = new HttpClient())
                {
                    string token = (string)Session["token"];

                    client.BaseAddress = new Uri(baseURL + "Client/NouveauIndex");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage getData = await client.GetAsync(client.BaseAddress);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        nouveauIndex = JsonConvert.DeserializeObject<String>(results);
                        Client client1 = new Client
                        {
                            code = nouveauIndex
                        };
                        return View(client1);
                    }
                    else
                    {
                        Console.WriteLine("Erreur calling web api");
                    }


                }
            }

            else
            {
                using (var client = new HttpClient())
                {
                    string token = (string)Session["token"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(c);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseURL + "Client");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage getData = await client.PostAsync("Client", data);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Create");

                    }
                    else
                    {

                        return View("ErreurCreation");
                    }
                }
            }
            return View(); 

        }
    
            public ActionResult Delete(string id)
        {
            string token = (string)Session["token"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44333/api/");

                // Ajout du Bearer Token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Devis/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

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
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    lstdevis = JsonConvert.DeserializeObject<List<Devis>>(results)
                .OrderByDescending(d => d.DATEBL)
                    .ToList();

                }
                
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewData.Model = lstdevis;

            }
            /////Charger point de vente 
            IList<UserPV> pointVentes = new List<UserPV>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("UserPV/GetUtilisateurpvs?codeuser=" + (string)Session["code"]);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Authentifier", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    pointVentes = JsonConvert.DeserializeObject<List<UserPV>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewBag.pvs = pointVentes;

            }


            return View("Index", lstdevis);
        }

       
        public async Task<ActionResult> Edit(string id, Devis model)
        {
            IList<Depot> depots = new List<Depot>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Depot/GetDepotsParUser?codeuser=" + (string)Session["code"]);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Authentifier", "Login");
                }
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
                    if ((int)response.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }



                    return RedirectToAction("Index");


                }

            }
            /////////
            Devis d = new Devis(); 
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Devis/GetDevis?id="+id);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    d = JsonConvert.DeserializeObject<Devis>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }

            }
            
           
            return View(d);
        }
        public async Task<ActionResult> AfficherLignes(string id)
        {
            Devis devis = new Devis();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri("http://localhost:44333/api/Devis?id=" + id);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync(client.BaseAddress);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    devis = JsonConvert.DeserializeObject<Devis>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewData.Model = devis;

            }

            return View();
        }

        public async Task<ActionResult> Create(Devis devis)
        {
            string nouveauIndex="";

            IList<Depot> depots = new List<Depot>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("Depot/GetDepotsParUser?codeuser=" + (string)Session["code"]);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Authentifier", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    depots = JsonConvert.DeserializeObject<List<Depot>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
            }
                ViewBag.codeDepot = new SelectList(depots, "code", "code");

            

            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                
                client.BaseAddress = new Uri(baseURL+"Devis/NouveauIndex");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync(client.BaseAddress);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Index", "Login");
                }
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
            IList<UserPV> pointVentes = new List<UserPV>();
            using (var client = new HttpClient())
            {
                string token = (string)Session["token"];
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage getData = await client.GetAsync("UserPV/GetUtilisateurpvs?codeuser=" + (string)Session["code"]);
                if ((int)getData.StatusCode == 401)
                {
                    return RedirectToAction("Authentifier", "Login");
                }
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    pointVentes = JsonConvert.DeserializeObject<List<UserPV>>(results);

                }
                else
                {
                    Console.WriteLine("Erreur calling web api");
                }
                ViewBag.pvs = new SelectList(pointVentes, "codepv", "libpv"); ;

            }
            if (devis.NUMBL != null)
            {



                using (var client = new HttpClient())
                {
                    string token = (string)Session["token"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var json = JsonConvert.SerializeObject(devis);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(baseURL + "Devis");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage getData = await client.PostAsync("Devis", data);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index");

                    }
                  
                }

            }
            LigneDevis ligneDevis = new LigneDevis();
            List<LigneDevis> lignes = new List<LigneDevis>();
            lignes.Add(ligneDevis);
            devis = new Devis
            {
                NUMBL = nouveauIndex,
                CODEFACTURE = "N",
                LignesDevis = lignes, 
                DATEBL = DateTime.Now,
                valorisation = "HT",
                usera= (string)Session["code"],
                TAUXREMISE=0

        };
            
            return View(devis);
        }
    }
}