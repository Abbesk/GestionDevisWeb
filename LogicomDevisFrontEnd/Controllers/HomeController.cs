using LogicomDevisFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LogicomDevisFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        string baseURL = "http://localhost:44333/api/";

        public async Task<ActionResult> Index()
        {
            if ((string)Session["token"] != null)
            {
                IList<Devis> lstDevis = new List<Devis>();
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    string token = (string)Session["token"];
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getData = await client.GetAsync("Devis");
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        lstDevis = JsonConvert.DeserializeObject<List<Devis>>(results);
                        //Moyenne d inventaires par semestre 
                        Dictionary<int, int> devisParMois = new Dictionary<int, int>();
                        int anneeEnCours = DateTime.Now.Year;
                        int nbDevisTotalAnneeEnCours = 0;
                        int nbDevisAnneeExecutes = 0;
                        int nbDevisAnneeOuverts = 0;

                        foreach (var devis in lstDevis)
                        {
                            if (devis.DATEBL.Value.Year == anneeEnCours)
                            {
                                nbDevisTotalAnneeEnCours++;
                                int moisDevis = devis.DATEBL.Value.Month;
                                if (devisParMois.ContainsKey(moisDevis))
                                {
                                    devisParMois[moisDevis]++;
                                }
                                else
                                {
                                    devisParMois[moisDevis] = 1;
                                }

                                if (devis.executer == "G")
                                {
                                    nbDevisAnneeExecutes++;
                                }
                                else
                                {
                                    nbDevisAnneeOuverts++;
                                }
                            }
                        }

                        int totalDevis = nbDevisTotalAnneeEnCours;
                        string moyenneDevis = ((double)totalDevis / 12).ToString("0.0");
                        ViewBag.MoyenneParMOIS = moyenneDevis;
                        //Nombre d inventaires  
                        ViewBag.nbDevisTotalAnneeEnCours = nbDevisTotalAnneeEnCours;
                        ViewBag.nbDevisAnneeExecutes = nbDevisAnneeExecutes;
                        ViewBag.nbDevisAnneeOuverts = nbDevisAnneeOuverts;
                        ViewBag.lstDevis = lstDevis;
                    }
                    else
                    {
                        Console.WriteLine("Erreur calling web api");
                    }

                    return View();

                }
            }
            else
            {
                return RedirectToAction("Authentifier", "Login");
            }

        }


        

        public async Task<ActionResult> Contact()
        {
            string selectedSoc = (string)Session["SelectedSoc"];
            if ((string)Session["token"] != "")
            {
                
                using (var client = new HttpClient())
                {
                    string token = (string)Session["token"];

                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage getData = await client.GetAsync("SocieteERP/GetFonctionSociete?id=" + selectedSoc);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        ViewBag.fonction = JsonConvert.DeserializeObject<string>(results);

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
                    HttpResponseMessage getData = await client.GetAsync("SocieteERP/GetAdresseSociete?id=" + selectedSoc);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        ViewBag.adresse = JsonConvert.DeserializeObject<string>(results);

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
                    HttpResponseMessage getData = await client.GetAsync("SocieteERP/GetEmailSociete?id=" + selectedSoc);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        ViewBag.email = JsonConvert.DeserializeObject<string>(results);

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
                    HttpResponseMessage getData = await client.GetAsync("SocieteERP/GetTelSociete?id=" + selectedSoc);
                    if ((int)getData.StatusCode == 401)
                    {
                        return RedirectToAction("Index", "Login");
                    }
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        ViewBag.tel = JsonConvert.DeserializeObject<string>(results);

                    }
                    else
                    {
                        Console.WriteLine("Erreur calling web api");
                    }

                }
                ViewBag.Message = "Your contact page.";
                return View();
            }
            return RedirectToAction("Index", "Login");




        }
    }
}