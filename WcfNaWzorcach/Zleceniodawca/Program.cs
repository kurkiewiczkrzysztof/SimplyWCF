using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zleceniodawca
{
    class Program
    {
        public static Random random = new Random();
        static void Main(string[] args)
        {
            NadzorcaReference.NadzorcaClient nr = new NadzorcaReference.NadzorcaClient();
            NadzorcaReference.Harmonogram har = new NadzorcaReference.Harmonogram();
            har.listaZadan = Generuj().ToArray();
            nr.WstawZlecenie(har);
            PoliczCalyKoszt(har);
            Console.WriteLine("Mój harmonogram " + har.kosztCalkowity);
            do
            {
                har = nr.PobierzWynik();
                Thread.Sleep(1000);
            } while (har == null);
            Console.WriteLine("Dostałem najlepszy:" + har.kosztCalkowity);
            Console.ReadKey();
        }
        private static void PoliczCalyKoszt(NadzorcaReference.Harmonogram harmonogram)
        {
            harmonogram.kosztCalkowity = 0;
            for (int i = 0; i < harmonogram.listaZadan.Length - 1; i++)
            {
                harmonogram.kosztCalkowity += Compare(harmonogram.listaZadan[i], harmonogram.listaZadan[i + 1]);
            }
        }
        private static decimal Compare(NadzorcaReference.Zadanie zadanie1, NadzorcaReference.Zadanie zadanie2)
        {
            decimal zmianaGatunku = 2000.0M;
            decimal zmianaFormatu = 1000.0M;
            decimal zmianaWszystkiego = 2200.0M;

            if (!zadanie1._format.Equals(zadanie2._format) && zadanie1._gatunek.Equals(zadanie2._gatunek))
            {
                return zmianaFormatu;
            }
            else if (!zadanie1._gatunek.Equals(zadanie2._gatunek) && zadanie1._format.Equals(zadanie2._format))
            {
                return zmianaGatunku;
            }
            else if (!zadanie1._gatunek.Equals(zadanie2._gatunek) && !zadanie1._format.Equals(zadanie2._format))
            {
                return zmianaWszystkiego;
            }
            else
            {
                return 0;
            }
        }
        static List<NadzorcaReference.Zadanie> Generuj()
        {
            List<NadzorcaReference.Zadanie> listaZadan = new List<NadzorcaReference.Zadanie>();
            for (int i = 0; i < 100;i++)
            {
                NadzorcaReference.Zadanie Zadaniaharmonogramu = new NadzorcaReference.Zadanie();
                Zadaniaharmonogramu._format = (NadzorcaReference.Format)random.Next(1, 3);
                Zadaniaharmonogramu._gatunek = (NadzorcaReference.Gatunek)random.Next(1, 3);
                Zadaniaharmonogramu._lD = DateTime.Now;
                listaZadan.Add(Zadaniaharmonogramu);
            }
            return listaZadan;
        }
    }
}
