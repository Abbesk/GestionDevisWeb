//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogicomDevisBackEnd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class article
    {
        public string code { get; set; }
        public string libelle { get; set; }
        public string unite { get; set; }
        public Nullable<double> nbrunite { get; set; }
        public string type { get; set; }
        public string fam { get; set; }
        public string fourn { get; set; }
        public string image { get; set; }
        public string chemin { get; set; }
        public Nullable<double> fodec { get; set; }
        public string tauxmajo { get; set; }
        public Nullable<double> remmax { get; set; }
        public Nullable<double> qtes { get; set; }
        public Nullable<double> stockinitial { get; set; }
        public Nullable<double> stockmin { get; set; }
        public Nullable<double> stockmax { get; set; }
        public Nullable<System.DateTime> dateachat { get; set; }
        public Nullable<double> DREMISE { get; set; }
        public Nullable<double> prixbrut { get; set; }
        public Nullable<double> prixnet { get; set; }
        public Nullable<double> marge { get; set; }
        public Nullable<double> margepv2 { get; set; }
        public Nullable<double> margepv3 { get; set; }
        public Nullable<System.DateTime> datevente { get; set; }
        public Nullable<double> prix1 { get; set; }
        public Nullable<double> prix2 { get; set; }
        public Nullable<double> prix3 { get; set; }
        public Nullable<double> prix4 { get; set; }
        public string CONFIG { get; set; }
        public string lieustock { get; set; }
        public string gestionstock { get; set; }
        public string libellef { get; set; }
        public string libellefourn { get; set; }
        public string avecconfig { get; set; }
        public string observation { get; set; }
        public Nullable<double> budget { get; set; }
        public Nullable<double> prixachini { get; set; }
        public string libelleAr { get; set; }
        public Nullable<double> prixdevice { get; set; }
        public string nomenclature { get; set; }
        public Nullable<double> PrixPub { get; set; }
        public Nullable<double> longeur { get; set; }
        public Nullable<double> largeur { get; set; }
        public Nullable<double> epaisseur { get; set; }
        public string Bois { get; set; }
        public string serie { get; set; }
        public string TYCODBAR { get; set; }
        public string codebarre { get; set; }
        public string gtaillecoul { get; set; }
        public Nullable<double> hauteur { get; set; }
        public Nullable<double> stkinitexer { get; set; }
        public Nullable<double> nbreptfid { get; set; }
        public string affectptfid { get; set; }
        public Nullable<System.DateTime> datprom1 { get; set; }
        public Nullable<System.DateTime> datprom2 { get; set; }
        public Nullable<double> remfidel { get; set; }
        public string NGP { get; set; }
        public Nullable<double> cours { get; set; }
        public Nullable<double> tariffrs { get; set; }
        public Nullable<System.DateTime> datetarif { get; set; }
        public string devise { get; set; }
        public Nullable<double> pxcomp { get; set; }
        public Nullable<int> cvlong { get; set; }
        public Nullable<int> cvlarg { get; set; }
        public string codefini { get; set; }
        public string libfini { get; set; }
        public string typeart { get; set; }
        public string reforigine { get; set; }
        public Nullable<double> puht { get; set; }
        public string avance { get; set; }
        public Nullable<System.DateTime> datecreate { get; set; }
        public string abrev { get; set; }
        public string abrevpart1 { get; set; }
        public string abrevpart2 { get; set; }
        public double dispo { get; set; }
        public Nullable<int> position { get; set; }
        public string import { get; set; }
        public string typeartg { get; set; }
        public Nullable<double> kmmax { get; set; }
        public Nullable<double> kmeff { get; set; }
        public Nullable<double> duregarant { get; set; }
        public string codefrs { get; set; }
        public string rsfrs { get; set; }
        public Nullable<System.DateTime> dernmajprix1 { get; set; }
        public Nullable<System.DateTime> dernmajprix2 { get; set; }
        public Nullable<System.DateTime> dernmajprix3 { get; set; }
        public string lib1 { get; set; }
        public string lib2 { get; set; }
        public string lib3 { get; set; }
        public string lib4 { get; set; }
        public Nullable<double> nbcart { get; set; }
        public string numlot { get; set; }
        public string qtecart { get; set; }
        public string qtesac { get; set; }
        public string unitegarantie { get; set; }
        public string usera { get; set; }
        public string userm { get; set; }
        public string users { get; set; }
        public Nullable<System.DateTime> datemaj { get; set; }
        public string dureealerte { get; set; }
        public string gestionlot { get; set; }
        public string artmouv { get; set; }
        public Nullable<double> pmp { get; set; }
        public string ventevrac { get; set; }
        public Nullable<double> prix1TTC { get; set; }
        public Nullable<double> prix2TTC { get; set; }
        public Nullable<double> prix3TTC { get; set; }
        public string genGPAO { get; set; }
        public string sav { get; set; }
        public string ftmodif { get; set; }
        public Nullable<double> prixsolde { get; set; }
        public Nullable<double> remisesolde { get; set; }
        public string cgrilletaille { get; set; }
        public string lgrilletaille { get; set; }
        public string ctaille { get; set; }
        public string taille { get; set; }
        public string libsousfam { get; set; }
        public string ccoul { get; set; }
        public string couleur { get; set; }
        public string ccol { get; set; }
        public string libcol { get; set; }
        public string sousfamille { get; set; }
        public string cons { get; set; }
        public Nullable<double> tcomm { get; set; }
        public Nullable<double> Dtcons { get; set; }
        public string codesousfam { get; set; }
        public double PUHTA { get; set; }
        public Nullable<double> PUHTV { get; set; }
        public string codepv { get; set; }
        public string libpv { get; set; }
        public Nullable<double> PrixMoyAchat { get; set; }
        public Nullable<double> PrixMoyVente { get; set; }
        public double mtcomm { get; set; }
        public Nullable<double> Poid { get; set; }
        public Nullable<double> PoidNet { get; set; }
        public string colisage { get; set; }
        public Nullable<int> imagesize { get; set; }
        public string imagepath { get; set; }
        public byte[] imagedata { get; set; }
        public string AveConsigne { get; set; }
        public string comptec { get; set; }
        public string sel { get; set; }
        public Nullable<double> tauxtva { get; set; }
    
        public virtual famille Famille { get; set; }
    }
}