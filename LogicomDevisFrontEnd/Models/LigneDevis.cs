using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LogicomDevisFrontEnd.Models
{
    public class LigneDevis
    {
        public string NumBL { get; set; }
        public Nullable<System.DateTime> DateBL { get; set; }
        public string CodeART { get; set; }
        
        public string DesART { get; set; }
        public Nullable<double> QteART { get; set; }
        public Nullable<double> PUART { get; set; }
        public Nullable<double> Remise { get; set; }
        public Nullable<double> TauxTVA { get; set; }
        public Nullable<double> TauxTVAB { get; set; }
        public string Unite { get; set; }
        public string TypeART { get; set; }
        public string CodeREP { get; set; }
        public Nullable<double> TTFodec { get; set; }
        public Nullable<double> TauxMAJO { get; set; }
        public string Conf { get; set; }
        public Nullable<double> taux { get; set; }
        public int NLigne { get; set; }
        public string famille { get; set; }
        public Nullable<float> tauxavance { get; set; }
        public Nullable<double> nbun { get; set; }
        public Nullable<int> num { get; set; }
        public string lot { get; set; }
        public string Item { get; set; }
        public string marque { get; set; }
        public string disponible { get; set; }
        public Nullable<double> mttc { get; set; }
        public Nullable<long> numseq { get; set; }
        public Nullable<double> qteliv { get; set; }
        public string codedep { get; set; }
        public string libdep { get; set; }
        public string typeapp { get; set; }
        public string origine { get; set; }
        public Nullable<double> QteACC { get; set; }
        public Nullable<System.DateTime> dateretenu { get; set; }
        public Nullable<sbyte> valeur { get; set; }
        public Nullable<double> Tcons { get; set; }
        public string codepv { get; set; }
        public Nullable<double> mtht { get; set; }

        public virtual Article Article { get; set; }
    }
}