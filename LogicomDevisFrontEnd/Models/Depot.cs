using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicomDevisFrontEnd.Models
{
    public class Depot
    {
        public string Code { get; set; }
        public string Libelle { get; set; }
        
        public  List<LigneDepot> LignesDepot { get; set; }
    }
}