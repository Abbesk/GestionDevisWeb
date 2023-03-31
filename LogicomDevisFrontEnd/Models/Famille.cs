using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicomDevisFrontEnd.Models
{
    public class Famille
    {
        public string code { get; set; }
        public string libelle { get; set; }
        public string achat { get; set; }
        public string vente { get; set; }
        public Nullable<double> dispo { get; set; }
        public string sav { get; set; }
        public string immeuble { get; set; }
        public Nullable<int> position { get; set; }
        public string codepv { get; set; }
        public string libpv { get; set; }
        public string favoris { get; set; }
    }
}