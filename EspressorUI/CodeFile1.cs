using System;

namespace Espressorlibrary
{
    class Cana
    {
        public bool pozitieCana { get; set; }
        public bool GetPozitieCana()
        {
            return pozitieCana;
        }
        public void AdaugaCana()
        {
            pozitieCana = true;
        }
        public void ScoateCana()
        {
            pozitieCana = false;
        }
    }

    class SuportCana
    {
        public int masaCana { get; set; }
        public int temperaturaCana { get; set; }

    }
    class Boiler
    {
        public int nivelApa { get; set; }
        public int temperaturaApa { get; set; }


    }
    public class Espressor
    {
        Cana cana = new Cana { pozitieCana = false };
        SuportCana suportCana = new SuportCana
        {
            masaCana = 0,
            temperaturaCana = 0
        };
        Boiler boiler = new Boiler
        {
            nivelApa = 0,
            temperaturaApa = 0
        };   
        string indicatorLight;
        int grameCafea=0;
        int presiune = 1;


        public void AdaugaApa()
        {
            boiler.nivelApa = 100;
            boiler.temperaturaApa = 25;
        }
        public int CitesteNivelApa()
        {
            return boiler.nivelApa;
        }
        public int CitesteTemperaturaApa()
        {
            return boiler.temperaturaApa;
        }
        public int SetTemeperaturaApa(int temperaturaDorita)
        {
            boiler.temperaturaApa = temperaturaDorita;
            return boiler.temperaturaApa;
        }
        public void AdaugaCafea(int grameCafeaAdaugate)
        {
            grameCafea = grameCafea + grameCafeaAdaugate;
        }
        public void ScoateCafea()
        {
            grameCafea = 0;
        }
        public int MasaCafea()
        {
            return grameCafea;
        }
        public int GetMasaCana()
        {
            if (!EsteCana()) return 0;
            return suportCana.masaCana;
        }
        public int TemperaturaCana()
        {
            if (!EsteCana()) return 0;
            return suportCana.temperaturaCana;
        }
        public void IncalzesteCana(int temperatura)
        {
            suportCana.temperaturaCana = temperatura;
        }
        public void GolesteCana()
        {
            suportCana.masaCana = 0;
        }
        public bool EsteCana()
        {
            return cana.pozitieCana;
        }
        public void SeteazaPresiune(int presiuneDorita)
        {
            if (presiuneDorita >= 9 && presiuneDorita <=14)
                presiune = presiuneDorita;
            else
                Console.WriteLine("Va rugam sa introduceti o valoare intre 9 si 14");
        }
        public int CitestePresiune()
        {
            return presiune;
        }
        public void AdaugaCana()
        {
            cana.AdaugaCana();
        }
        public void ScoateCana()
        {
            cana.ScoateCana();
        }
        public override string ToString()
        {
            string mesaj;
            if (EsteCana())
                mesaj = "Cana este in pozitie\n";
            else mesaj = "Cana nu este in pozitie\n";
            mesaj += "Masa cana: " + GetMasaCana().ToString() + " grame\n";
            mesaj += "Temperatura cana: " + TemperaturaCana().ToString() + " grade C\n";
            mesaj += "Nivel apa in boiler: " + CitesteNivelApa().ToString() + "%\n";
            mesaj += "Temperatura apa in boiler: " + CitesteTemperaturaApa().ToString() + " grade C\n";
            mesaj += "Masa Cafea: " + MasaCafea().ToString() + " grame\n";
            mesaj += "Presiune: " + CitestePresiune().ToString() + " bar\n";

            return mesaj;
        }
        public void Start()
        {
            boiler.nivelApa -= 2;
            suportCana.masaCana += 5;
        }

        public void Stop()
        {
            boiler.nivelApa -= 1;
            suportCana.masaCana += 2;
        }

    }
}