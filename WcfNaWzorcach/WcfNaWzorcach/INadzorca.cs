using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfNaWzorcach
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INadzorca" in both code and config file together.
    [ServiceContract]
    public interface INadzorca
    {
        [OperationContract]
        int PobierzLicznik();
        //Zleceniodawca
        [OperationContract]
        void WstawZlecenie(Harmonogram h);
        [OperationContract]
        Harmonogram PobierzWynik();
        [OperationContract]
        //Zleceniobiorca
        Harmonogram PobierzZlecenie();
        [OperationContract]
        void OddajWynikCzastkowy(Harmonogram h);
    }
    [DataContract]
    public enum Format
    {
        [EnumMember]
        Kolo,
        [EnumMember]
        Kwadrat,
        [EnumMember]
        Prostkoat
    }
    [DataContract]
    public enum Gatunek
    {
        [EnumMember]
        S03,
        [EnumMember]
        S200,
        [EnumMember]
        S1000
    }
    [DataContract]
    public class Harmonogram
    {
        [DataMember]
        List<Zadanie> listaZadan { get; set; }
        [DataMember]
        static Random generator { get; set; }
        [DataMember]
        public decimal kosztCalkowity { get; set; }

    }
    [DataContract]
    public class Zadanie
    {

        [DataMember]
        public Format _format { get; set; }
        [DataMember]
        public Gatunek _gatunek { get; set; }
        [DataMember]
        public DateTime _lD { get; set; }
        [DataMember]
        public static decimal _oplata { get; set; }
        
    }

}
