using LogicomDevisFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace LogicomDevisFrontEnd.Controllers
{
    public class LoginController : Controller
    {
        
        public async Task<ActionResult> Index(Utilisateur user)
        {
            if (user.codeuser !=null && user.motpasse!=null)
            {

            
            // Create HttpClient instance to call the web API
            using (HttpClient client = new HttpClient())
            {
                    // Set the base address of the web API

                    // Set the content of the request body
                    var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                // Send a post request to the authenticate endpoint of the web API
                HttpResponseMessage response = await client.PostAsync("http://localhost:44333/Api/Utilisateur/Login", content);

                // If the response is successful, store the token in a cookie and redirect to the home page
                if (response.IsSuccessStatusCode)
                {
                        string token = (await response.Content.ReadAsStringAsync()).Trim('"');
                        Session["token"] = token;
                        HttpCookie cookie = new HttpCookie("token", token);
                        Response.Cookies.Add(cookie);

                        // Decode the token to retrieve the user name

                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(token);
                        string Role = jsonToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                        string id = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                        string name = jsonToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                        string Societe = jsonToken.Claims.FirstOrDefault(c => c.Type == "website")?.Value;

                        // Store the user name in the ViewBag for use in the navbar
                        Session["name"] = name;
                        Session["societe"] = Societe;
                        return RedirectToAction("Index", "Home");
                    }
                // If the response is not successful, display an error message
                else
                {
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View(user);
                }
            }
        }
            return View(); 
        }

        
        public async Task<ActionResult> Logout()
        {
            // Get the token from the cookie
            var token = Request.Cookies["token"]?.Value;

            // Create an HTTP client
            using (var client = new HttpClient())
            {
                
                
                Session["token"] = token;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await client.PostAsync("http://localhost:44333/Api/Utilisateur/Logout", null);
                if (response.IsSuccessStatusCode)
                {  
                    Response.Cookies.Remove("token");
 
                    return RedirectToAction("Index", "Login");
                }
                else
                {     
                    return View();
                }
            }
        }



    }
}