using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicomDevisFrontEnd.Models
{
    public class Devis
    {
        public string NUMBL { get; set; }
        public string ADRCLI { get; set; }
        public string CODECLI { get; set; }
        public string CODEFACTURE { get; set; }
        public string CP { get; set; }
        public string COM1 { get; set; }
        public string COM2 { get; set; }
        public string COM3 { get; set; }
        public Nullable<System.DateTime> DATEBL { get; set; }
        public string MODEREG { get; set; }
        public Nullable<double> MREMISE { get; set; }
        public Nullable<double> MTTC { get; set; }
        public Nullable<double> MTVA { get; set; }
        public Nullable<double> BASE1 { get; set; }
        public Nullable<double> BASE2 { get; set; }
        public Nullable<double> BASE3 { get; set; }
        public Nullable<double> BASE4 { get; set; }
        public Nullable<double> BASE5 { get; set; }
        public Nullable<double> TVA1 { get; set; }
        public Nullable<double> TVA2 { get; set; }
        public Nullable<double> TVA3 { get; set; }
        public Nullable<double> TVA4 { get; set; }
        public Nullable<double> TVA5 { get; set; }
        public string RSCLI { get; set; }
        public Nullable<double> TAUXREMISE { get; set; }
        public Nullable<double> MHT { get; set; }
        public string EXON { get; set; }
        public Nullable<double> MFODEC { get; set; }
        public Nullable<double> BFODEC { get; set; }
        public Nullable<double> RETENUE { get; set; }
        public string CEXPORT { get; set; }
        public string CODEREP { get; set; }
        public string RSREP { get; set; }
        public string CODETVA { get; set; }
        public Nullable<double> MAJ1 { get; set; }
        public Nullable<double> MAJ2 { get; set; }
        public Nullable<double> MAJ3 { get; set; }
        public Nullable<double> MAJ4 { get; set; }
        public Nullable<double> MAJ5 { get; set; }
        public Nullable<double> TIMBRE { get; set; }
        public Nullable<double> MAJO { get; set; }
        public Nullable<double> TREMISE { get; set; }
        public string FACTURE { get; set; }
        public string CODEVAL { get; set; }
        public string CODESEL { get; set; }
        public Nullable<double> TVADUE { get; set; }
        public string CODERAP { get; set; }
        public string BONINTER { get; set; }
        public string CODECHA { get; set; }
        public string DESICHA { get; set; }
        public string AGENT { get; set; }
        public Nullable<double> FODECDUE { get; set; }
        public string NUMBC { get; set; }
        public Nullable<double> TGARANTIE { get; set; }
        public Nullable<double> TRSOURCE { get; set; }
        public Nullable<double> MGARANTIE { get; set; }
        public Nullable<System.DateTime> DATEDMAJ { get; set; }
        public string DECISION { get; set; }
        public string REFCOMM { get; set; }
        public string comm { get; set; }
        public string commint { get; set; }
        public string delailivr { get; set; }
        public string transport { get; set; }
        public string modepaie { get; set; }
        public string usera { get; set; }
        public string userm { get; set; }
        public string users { get; set; }
        public Nullable<double> baseavance { get; set; }
        public Nullable<double> mtavance { get; set; }
        public string descriptiontxt { get; set; }
        public string Description { get; set; }
        public string mlettre { get; set; }
        public Nullable<System.DateTime> datt { get; set; }
        public string executer { get; set; }
        public Nullable<double> mnht { get; set; }
        public string confcl { get; set; }
        public string confcltxt { get; set; }
        public string codsui { get; set; }
        public string dessui { get; set; }
        public string contacte { get; set; }
        public string commentaire { get; set; }
        public string mtlettre { get; set; }
        public string nb { get; set; }
        public Nullable<double> remglo { get; set; }
        public string commentete { get; set; }
        public string modeliv { get; set; }
        public string valoff { get; set; }
        public Nullable<float> arrondi { get; set; }
        public string champ1 { get; set; }
        public string champ2 { get; set; }
        public string champ3 { get; set; }
        public string champ4 { get; set; }
        public string champ5 { get; set; }
        public string champ6 { get; set; }
        public string champ7 { get; set; }
        public string champ8 { get; set; }
        public string champ9 { get; set; }
        public string champ10 { get; set; }
        public string champ11 { get; set; }
        public string affichec { get; set; }
        public string affichet { get; set; }
        public string affiched { get; set; }
        public string papierb { get; set; }
        public string codepv { get; set; }
        public string libpv { get; set; }
        public string typeapp { get; set; }
        public string cloturer { get; set; }
        public Nullable<System.DateTime> datelimit { get; set; }
        public string valorisation { get; set; }
        public string PFvalide { get; set; }
        public string PFmodif { get; set; }
        public string retenuefp { get; set; }
        public string subv { get; set; }
        public string cred { get; set; }
        public string etatcl { get; set; }
        public double Bcons { get; set; }
        public double Mcons { get; set; }
        public string duree1 { get; set; }
        public string duree2 { get; set; }
        public string TEMPDMAJ { get; set; }
        public string codesecteur { get; set; }
        public Nullable<double> puissance { get; set; }
        public string reference { get; set; }

        public virtual Client client { get; set; }
      
        public List<LigneDevis> LignesDevis { get; set; } 
        public virtual PointVente PointVente { get; set; }
    }
}