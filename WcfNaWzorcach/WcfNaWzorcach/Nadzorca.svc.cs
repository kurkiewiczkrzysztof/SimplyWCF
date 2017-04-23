using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfNaWzorcach
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Nadzorca" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Nadzorca.svc or Nadzorca.svc.cs at the Solution Explorer and start debugging.
    public class Nadzorca : INadzorca
    {
        static int licznik = 0;
        public int PobierzLicznik()
        {
            licznik++;
            return licznik;
        }

        static Harmonogram pierwotnyHarmonogram = null;
        static List<Harmonogram> policzoneHarmonogramy = new List<Harmonogram>();
        private static Random rand = new Random();
        public void WstawZlecenie(Harmonogram h)
        {
            pierwotnyHarmonogram = h;
        }

        public Harmonogram PobierzWynik()
        {
            if(policzoneHarmonogramy.Count>=10)
            {
                return najlepszyHarmonogram(policzoneHarmonogramy);
            }
            else
            {
                return null;
            }
        }

        public Harmonogram PobierzZlecenie()
        {
            return pierwotnyHarmonogram;
        }

        public void OddajWynikCzastkowy(Harmonogram h)
        {
            policzoneHarmonogramy.Add(h);
        }
        static Harmonogram najlepszyHarmonogram(List<Harmonogram> harmonogram)
        {
            Harmonogram Najlepszy = null;
            for(int i=0;i<harmonogram.Count;i++)
            {
                if(Najlepszy.Equals(null)||harmonogram[i].kosztCalkowity<Najlepszy.kosztCalkowity)
                {
                    Najlepszy = harmonogram[i];
                }
            }
            Console.WriteLine("Najlepszy harmonogram " + Najlepszy.ToString());
            return Najlepszy;
        }
    }
}
