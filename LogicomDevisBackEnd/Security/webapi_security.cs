using LogicomDevisBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicomDevisBackEnd.Security
{
    public class webapi_security
    {
    public static bool ValidateUsers(string username, string user_password)
        {
            usererpEntities1 db = new usererpEntities1();
            var result = db.utilisateur.Where(x => x.codeuser == username && x.motpasse == user_password).FirstOrDefault();
            if (result != null)
            {
                return true; 

            }
            else
            {
                return false;
            }
        }
    }
}