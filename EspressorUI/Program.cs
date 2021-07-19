using System;
using Espressorlibrary;


namespace EspressorUI
{
    public enum Stari
    {
        Stare_initiala,
        Cana,
        Apa,
        Cafea,
        Cana_incalzita,
        Cana_Apa,
        Cana_Cafea,
        Cana_incalzita_Apa,
        Cana_incalzita_Cafea,
        Cana_incalzita_Apa_incalzita,
        Cana_incalzita_Apa_Cafea,
        Cana_incalzita_Apa_incalzita_Presiune,
        Cana_incalzita_Apa_incalzita_Cafea,
        Cana_incalzita_Apa_incalzita_Cafea_Presiune,
        Filtrare,
        STOP,
        Cana_Apa_incalzita,
        Cana_Apa_Cafea,
        Cana_Apa_incalzita_Presiune,
        Cana_Apa_incalzita_Cafea,
        Cana_Apa_incalzita_Cafea_Presiune,
        Apa_Cafea,
        Apa_incalzita,
        Apa_incalzita_Cafea,
        Apa_incalzita_Cafea_Presiune,
        Apa_incalzita_Presiune
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Espressor espressor = new Espressor();
            string raspuns;
            Stari stare = Stari.Stare_initiala;
            while(true)
            {
                Console.WriteLine(espressor);
                switch (stare)
                {
                    case Stari.Stare_initiala:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if(raspuns  == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Apa;
                            }
                            if(raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana;
                            }
                            if(raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cafea;
                            }
                            break;
                        }
                    case Stari.Cana:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Adauga cafea");
                            Console.WriteLine("Incalzeste cana");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Cana_Apa;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Stare_initiala;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_Cafea;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita;
                            }

                            break;
                        }
                    case Stari.Apa:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Adauga cafea");
                            Console.WriteLine("Incalzeste apa");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Apa_Cafea;
                            }
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Apa_incalzita;
                            }


                            break;
                        }
                    case Stari.Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if(raspuns == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Apa_Cafea;
                            }
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Cafea;
                            }
                            if(raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Stare_initiala;
                            }

                            break;
                        }
                    case Stari.Cana_incalzita:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Cana_incalzita_Apa;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Stare_initiala;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_incalzita_Cafea;
                            }
                            break;
                        }
                    case Stari.Cana_Apa:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Adauga cafea");
                            Console.WriteLine("Incalzeste apa");
                            Console.WriteLine("Incalzeste cana");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_Apa_Cafea;
                            }
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Cana_Apa_incalzita;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa;
                            }
                            break;
                        }
                    case Stari.Cana_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Cafea;
                            }
                            if (raspuns == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Cana_Apa_Cafea;
                            }
                            break;
                        }
                    case Stari.Cana_incalzita_Apa:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Incalzeste apa");
                            Console.WriteLine("Adauga cafea");
                            Console.WriteLine("Scoate cana");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa;
                            }
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Cana_incalzita_Apa_incalzita;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_incalzita_Apa_Cafea;
                            }

                            break;
                        }
                    case Stari.Cana_incalzita_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Adauga apa");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Adauga apa")
                            {
                                espressor.AdaugaApa();
                                stare = Stari.Cana_incalzita_Apa_Cafea;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_incalzita;
                            }
                            break;
                        }
                    case Stari.Cana_incalzita_Apa_incalzita:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Seteaza presiunea");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Seteaza presiunea")
                            {
                                espressor.SeteazaPresiune(9);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Presiune;
                            }
                            break;
                        }
                    case Stari.Cana_incalzita_Apa_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Incalzeste apa");
                            Console.WriteLine("Scoate cafea");
                            Console.WriteLine("Scoate cana");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_incalzita_Apa;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_Cafea;
                            }
                            break;
                        }
                    case Stari.Cana_incalzita_Apa_incalzita_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea_Presiune;
                            }

                            break;
                        }
                    case Stari.Cana_incalzita_Apa_incalzita_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Seteaza presiunea");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Seteaza presiunea")
                            {
                                espressor.SeteazaPresiune(9);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea_Presiune;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_incalzita_Apa_incalzita;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Cafea;
                            }
                            break;
                        }
                    case Stari.Cana_incalzita_Apa_incalzita_Cafea_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Scoate cafea");
                            Console.WriteLine("Start");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_incalzita_Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Cafea_Presiune;
                            }
                            if (raspuns == "Start")
                            {
                                espressor.Start();
                                stare = Stari.Filtrare;
                            }

                            break;
                        }
                    case Stari.Filtrare:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Stop");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Stop")
                            {
                                espressor.Stop();
                                stare = Stari.STOP;
                            }
                            break;
                        }
                    case Stari.STOP:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Start");
                            Console.WriteLine("Alt");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Start")
                            {
                                espressor.Start();
                                stare = Stari.Filtrare;
                            }
                            if (raspuns == "Alt")
                            {
                                espressor.ScoateCana();
                                espressor.ScoateCafea();
                                if (espressor.CitesteNivelApa() < 20)
                                {
                                    stare = Stari.Stare_initiala;
                                    break;
                                }
                                stare = Stari.Apa_incalzita_Presiune;
                            }
                            break;
                        }
                    case Stari.Cana_Apa_incalzita:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Seteaza presiunea");
                            Console.WriteLine("Adauga cafea");
                            Console.WriteLine("Scoate cana");
                            raspuns = Console.ReadLine();

                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa_incalzita;
                            }
                            if (raspuns == "Seteaza presiunea")
                            {
                                espressor.SeteazaPresiune(9);
                                stare = Stari.Cana_Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita;
                            }
                            break;
                        }
                    case Stari.Cana_Apa_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Incalzeste apa");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa_Cafea;
                            }
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Cana_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_Apa;
                            }
                            break;
                        }
                    case Stari.Cana_Apa_incalzita_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Cana_Apa_incalzita_Cafea;
                            }

                            break;
                        }
                    case Stari.Cana_Apa_incalzita_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Scoate cafea");
                            Console.WriteLine("Seteaza presiunea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_Apa_incalzita;
                            }
                            if (raspuns == "Seteaza presiunea")
                            {
                                espressor.SeteazaPresiune(9);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea_Presiune;
                            }
                            break;
                        }
                    case Stari.Cana_Apa_incalzita_Cafea_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Scoate cana");
                            Console.WriteLine("Incalzeste cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Scoate cana")
                            {
                                espressor.ScoateCana();
                                stare = Stari.Apa_incalzita_Cafea_Presiune;
                            }
                            if (raspuns == "Incalzeste cana")
                            {
                                espressor.IncalzesteCana(40);
                                stare = Stari.Cana_incalzita_Apa_incalzita_Cafea_Presiune;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Cana_Apa_incalzita_Presiune;
                            }

                            break;
                        }
                    case Stari.Apa_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Incalzeste Apa");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Incalzeste apa")
                            {
                                espressor.SetTemeperaturaApa(70);
                                stare = Stari.Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Apa;
                            }
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa_Cafea;
                            }
                            break;
                        }
                    case Stari.Apa_incalzita:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa_incalzita;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Apa_incalzita_Cafea;
                            }
                            break;
                        }
                    case Stari.Apa_incalzita_Cafea:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Scoate cafea");
                            Console.WriteLine("Seteaza presiunea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa_incalzita_Cafea;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Apa_incalzita;
                            }
                            if (raspuns == "Seteaza presiunea")
                            {
                                espressor.SeteazaPresiune(9);
                                stare = Stari.Apa_incalzita_Cafea_Presiune;
                            }

                            break;
                        }
                    case Stari.Apa_incalzita_Cafea_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Scoate cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa_incalzita_Cafea_Presiune;
                            }
                            if (raspuns == "Scoate cafea")
                            {
                                espressor.ScoateCafea();
                                stare = Stari.Apa_incalzita_Presiune;
                            }
                            break;
                        }
                    case Stari.Apa_incalzita_Presiune:
                        {
                            Console.WriteLine("Comenzi disponibile: ");
                            Console.WriteLine("Pune cana");
                            Console.WriteLine("Adauga cafea");
                            raspuns = Console.ReadLine();
                            if (raspuns == "Pune cana")
                            {
                                espressor.AdaugaCana();
                                stare = Stari.Cana_Apa_incalzita_Presiune;
                            }
                            if (raspuns == "Adauga cafea")
                            {
                                espressor.AdaugaCafea(18);
                                stare = Stari.Apa_incalzita_Cafea_Presiune;
                            }
                            break;
                        }
                }
            }
        }
    }
}
