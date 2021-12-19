using Payement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayementConsole
{
    class Program
    {
        private static List<Guest> listGuest { get; set; } = new List<Guest>();
        private static PayementService srv = new PayementService();

        static string Menu()
        {
            Console.WriteLine("|================|");
            Console.WriteLine("|  Top payement  |");
            Console.WriteLine("|================|\n");
            Console.WriteLine("1 - Nouvelle soirée");
            Console.WriteLine("2 - Voir les soirées existantes");
            Console.WriteLine("3 - Modifier une soirée");
            Console.WriteLine("4 - Quitter\n");

            var key = Console.ReadLine();

            return key;
        }

        static string MenuNewParty() {
            Console.WriteLine("1 - Ajouter un invité");
            Console.WriteLine("2 - Calculer les dettes\n");

            var key = Console.ReadLine();

            return key;
        }

        static string MenuModifyParty()
        {
            Console.WriteLine("1 - Ajouter un invité");
            Console.WriteLine("2 - Changer le nom");
            Console.WriteLine("3 - Calculer les dettes");
            Console.WriteLine("4 - Supprimer");
            Console.WriteLine("5 - Quitter\n");

            var key = Console.ReadLine();

            return key;
        }

        static void calc(int partyID)
        {
            listGuest.Clear();
            var guests = srv.GetAllGuestsFromParty(partyID);
            guests.ForEach(g => listGuest.Add(g));
            if (listGuest.Count < 2)
                Console.WriteLine("Veuillez entrer au moins 2 utilisateurs");
            
            // Calcul
            
            Dictionary<int, float> dettes = new Dictionary<int, float>(); // key = id / value = spent
            Dictionary<int, string> planFinal = new Dictionary<int, string>(); // string order : IDdette-IDpreteur-Dette

            // moyenne
            float moyenne = 0.0f;
            listGuest.ForEach(g => moyenne += g.Spent);
            moyenne /= listGuest.Count;

            listGuest.ForEach(g =>
            {
                // si dette negative alors c'est ce qu'il a payé en trop (découvert)
                dettes.Add(g.ID, moyenne - g.Spent);
            });

            int iterator = 0;
            foreach (var preteur in dettes.OrderBy(key => key.Value)) // plus grand au plus petit
            {
                foreach (var dette in dettes.OrderByDescending(key => key.Value)) // plus petit au plus grand
                {
                    if (dette.Key != preteur.Key && dette.Value>0 && preteur.Value<0)
                    {
                        dettes[dette.Key] -= preteur.Value;
                        planFinal.Add(iterator, $"{dette.Key}-{preteur.Key}-{dette.Value}");
                        dettes[preteur.Key] -= preteur.Value;
                        iterator++;
                    }
                }
            }

            for (int i = 0; i<iterator; i++)
            {
                var tab = planFinal[i].Split("-");

                //On sépare le tableau pour obtenir l'ID du preteur de l'endété et la dette
                int detteID = Convert.ToInt32(tab[0]);
                int preteurID = Convert.ToInt32(tab[1]);
                float dette = float.Parse(tab[2]);

                var UsernameDette = guests.Find(g => g.ID == detteID).Username;
                var UsernamePreteur = guests.Find(g => g.ID == preteurID).Username;
                Console.WriteLine($"{UsernameDette} doit {dette} euros à {UsernamePreteur}");
            }
            Console.WriteLine("\n");
        }

        static Guest newGuest(int Pid)
        {
            Console.WriteLine("Veuillez entrer un nom :");
            var u = Console.ReadLine();
            Console.WriteLine("Veuillez entrer la somme dépensée :");
            var s = float.Parse(Console.ReadLine());
            var g = new Guest(Pid, u, s);

            g = srv.InsertGuest(g);
            return g;
        }

        static void CreateParty()
        {
            bool exit = false;
            Console.WriteLine("Veuillez choisir un nom : ");
            var username = Console.ReadLine();

            var p = new Party(username);
            p = srv.InsertParty(p);

            //menu NewParty
            while (!exit)
            {
                var key = MenuNewParty();
                //New Guest
                if (key == "1")
                {
                    Guest g = newGuest(p.ID);
                    listGuest.Add(g);
                }
                //Calculer dettes
                else if (key == "2")
                {
                    calc(p.ID);
                }
            }
            Console.WriteLine("\n");
        }

        static void SeeParty()
        {
            var parties = srv.GetAllParties();
            parties.ForEach(p =>
            {
                Console.WriteLine(p.ToString());
            });
            Console.WriteLine("\n");
        }

        static void ChangePartyNickname(int id, string name)
        {
            var p = srv.GetPartyByID(id);
            p.Name = name;
            srv.UpdateParty(p);
        }

        static void DeleteParty(int id)
        {
            srv.DeleteParty(id);
        }

        static void ModifyParty(string Pid)
        {
            bool exit = false;
            while (!exit)
            {
                var key = MenuModifyParty();
                switch (key)
                {
                    default:
                        break;
                    case "1":
                        //add guest
                        Guest g = newGuest(Convert.ToInt32(Pid));
                        break;
                    case "2":
                        //change name
                        Console.WriteLine("Veuillez entrer le nouveau nom");
                        string name = Console.ReadLine();
                        ChangePartyNickname(Convert.ToInt32(Pid), name);
                        break;
                    case "3":
                        // calc
                        calc(Convert.ToInt32(Pid));
                        break;
                    case "4":
                        // delete
                        DeleteParty(Convert.ToInt32(Pid));
                        Console.WriteLine("Partie supprimée");
                        exit = true;
                        break;
                    case "5":
                        // quitter
                        exit = true;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                string key = Menu();
                switch (key)
                {
                    default:
                        break;
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Soirée créée !\n");
                        CreateParty();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Liste des soirées :\n");
                        SeeParty();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Modifier une soirée :\n");
                        Console.WriteLine("Veuillez entrer un ID :\n");
                        var id = Console.ReadLine();
                        ModifyParty(id);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Quitter");
                        exit = true;
                        break;
                    case "9":
                        Console.Clear();
                        calc(10);
                        break;
                }
            }
        }
    }
}
