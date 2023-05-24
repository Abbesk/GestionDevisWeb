using LogicomDevisBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicomDevisBackEnd.Service
{
    public class UserService
    {
        public utilisateur GetUserByCredentials(string username , string password)
        {
            if(username != "email@domain.com" || password != "password")
            {
                return null; 
            }
            utilisateur utilisateur = new utilisateur() { codeuser = "email@domain.com", motpasse = "password" };
            if (utilisateur != null)
            {
                utilisateur.motpasse = string.Empty , 
            }
            return utilisateur; 
        }

    }
}