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
        public string Adresse { get; set; }
        public string Responsable { get; set; }
        public Nullable<System.DateTime> Datec { get; set; }
        public string TEL { get; set; }
        public string FAX { get; set; }
        public string EMAIL { get; set; }
        public string TYPED { get; set; }
        public string codepv { get; set; }
        public string libpv { get; set; }
        public string inactif { get; set; }
        public Nullable<int> sel { get; set; }
        public string SAISIQTENEG { get; set; }
        public  List<LigneDepot> LignesDepot { get; set; }
    }
}