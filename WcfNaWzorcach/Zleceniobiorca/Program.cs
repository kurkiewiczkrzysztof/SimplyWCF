using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zleceniobiorca
{
    class Program
    {
        private static Random rand = new Random();
        static void Main(string[] args)
        {
            NadzorcaReference.NadzorcaClient nr = new NadzorcaReference.NadzorcaClient();
            NadzorcaReference.Harmonogram har = new NadzorcaReference.Harmonogram();
            while (true)
            {

                har = nr.PobierzZlecenie();
                if (har != null)
                {
                    Console.WriteLine("Odebrano zadanie");
                    har = Optymalizator(har, 1000);
                    nr.OddajWynikCzastkowy(har);
                    Console.WriteLine("Wysylam");
                }
                Thread.Sleep(1000);
            }
        }
        private static NadzorcaReference.Harmonogram Optymalizator(NadzorcaReference.Harmonogram harmonogram,int max)
        {
            NadzorcaReference.Harmonogram najlepszyHarmonogram = null;
            for(int i=0;i<max;i++)
            {
                PoliczCalyKoszt(harmonogram);
                if (najlepszyHarmonogram == null || harmonogram.kosztCalkowity < najlepszyHarmonogram.kosztCalkowity)
                {
                    najlepszyHarmonogram = Clone(harmonogram);
                }
                Shuffle(harmonogram);
            }
            return najlepszyHarmonogram;
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
        private static void PoliczCalyKoszt(NadzorcaReference.Harmonogram harmonogram)
        {
            harmonogram.kosztCalkowity = 0;
            for(int i=0;i<harmonogram.listaZadan.Length-1;i++)
            {
                harmonogram.kosztCalkowity += Compare(harmonogram.listaZadan[i], harmonogram.listaZadan[i + 1]);
            }
        }
        private static NadzorcaReference.Harmonogram Clone(NadzorcaReference.Harmonogram harmonogram)
        {
            return (new NadzorcaReference.Harmonogram()
            {
                listaZadan = (NadzorcaReference.Zadanie[])harmonogram.listaZadan.Clone(),
                kosztCalkowity = harmonogram.kosztCalkowity
            });
        }
        private static void Shuffle(NadzorcaReference.Harmonogram harmonogram)
        {
            harmonogram.listaZadan = harmonogram.listaZadan.OrderBy(c => rand.Next()).ToArray();
        }

    }
}