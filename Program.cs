using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgettoSettimanale1_Contribuente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GeneraDichiarazione();
        }

        public static void GeneraDichiarazione()
        {
            Console.WriteLine("");
            Console.WriteLine("==============OPERAZIONI===============");
            Console.WriteLine("Scegli l'operazione da effettuare:");
            Console.WriteLine("1.: Inserisci Dichiarazione");
            Console.WriteLine("2.: Lista contribuenti");
            var scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    User.Login();
                    break;
                case "2":
                    ListAccessi.PrintTableLoggedStory();
                    break;
            }
            GeneraDichiarazione();
        }

    }

    public static class User
    {
        public static string Nome;
        public static string Cognome;
        public static string DataNascita;
        public static string CodiceFiscale;
        public static string Sesso;
        public static string ComuneResidenza;
        public static double RedditoAnnuale;
        public static double ImpostaVersamento;
        public static bool IsLogged;
        public static DateTime DataOraLog;
        
        public static void Login()
        {
            Console.WriteLine("Inserisci il tuo nome:");
            Nome = Console.ReadLine();

            Console.WriteLine("Inserisci il tuo cognome:");
            Cognome = Console.ReadLine();

            Console.WriteLine("Inserisci la tua data di nascita:");
            DataNascita = Console.ReadLine();

            Console.WriteLine("Inserisci il tuo codice fiscale:");
            CodiceFiscale = Console.ReadLine();

            /* if (CodiceFiscale.Length != 16)
            {
                Console.WriteLine("Controllare Codice Fiscale - Non è possibile registrare l'utente");
                Logout();
            }
            else
            {
                return;
            */

            Console.WriteLine("Come ti identifichi? (M/F/Altro)");
            Sesso = Console.ReadLine();

            Console.WriteLine("Inserisci il tuo comune di residenza:");
            ComuneResidenza = Console.ReadLine();

            Console.WriteLine("Inserisci il tuo reddito annuale:");
            double RedditoAnnuale = Convert.ToDouble(Console.ReadLine());

            double ImpostaVersamento = 0;
     
            if ( RedditoAnnuale <= 15000)
            { ImpostaVersamento = RedditoAnnuale * 0.23; }
            else if ((RedditoAnnuale > 15000) && (RedditoAnnuale <= 28000)) {  ImpostaVersamento = 3450 + ((RedditoAnnuale - 15000) * 0.27); }
            else if ((RedditoAnnuale > 28000) && (RedditoAnnuale <= 55000)) {  ImpostaVersamento = 6960 + ((RedditoAnnuale - 28000) * 0.38); }
            else if ((RedditoAnnuale > 55000) && (RedditoAnnuale <= 75000)) {  ImpostaVersamento = 17220 + ((RedditoAnnuale - 55000) * 0.41); }
            else { ImpostaVersamento = 25420 + ((RedditoAnnuale - 75000) * 0.43); }
      

            if ((User.Nome != "") && (User.Cognome != "") && (User.ComuneResidenza != "") && (User.CodiceFiscale != ""))
            {
                IsLogged = true;
                DataOraLog = DateTime.Now;
                Console.WriteLine($"Utente correttamente loggato in data {DataOraLog.ToShortDateString()} alle ore {DataOraLog.ToShortTimeString()}");
                Console.WriteLine(" ");
                Console.WriteLine("CALCOLO DELL’IMPOSTA DA VERSARE:");
                Console.WriteLine($"Contribuente: {Nome} {Cognome}");
                Console.WriteLine($"Nato/a: {DataNascita} ({Sesso}) ");
                Console.WriteLine($"Residente in: {ComuneResidenza}");
                Console.WriteLine($"Codice Fiscale: {CodiceFiscale}");
                Console.WriteLine($"Reddito Dichiarato: {RedditoAnnuale}  € ");
                Console.WriteLine($"IMPOSTA DA VERSARE: {ImpostaVersamento}  €");

                UsersLogged users = new UsersLogged() { Username = User.Nome, Surname = User.Cognome, Imposta = ImpostaVersamento, DateTimeLogged = DataOraLog };
                ListAccessi.users.Add(users);
            }
            else
            {
                Console.WriteLine("Non è possibile effettuare il login");
            }

        }

       
       public static void Logout()
        {
            Nome = "";
            Cognome = "";
            DataNascita = "";
            CodiceFiscale = "";
            Sesso = "";
            ComuneResidenza = "";
            RedditoAnnuale = 0;
            IsLogged = false;
            Console.WriteLine("Nessun utente loggato al sistema");
        }

        public static void StampaDataEOraLogin()
        {
            if (User.IsLogged == true)
            {
                Console.WriteLine($"L'utente {User.Nome} {User.Cognome} ha effettuato l'accesso il {User.DataOraLog.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("Non risultano utenti loggati a sistema");
            }
        }
    }

    public class UsersLogged
    {
        public string Username { get; set; }

        public string Surname { get; set; }

        public double Imposta { get; set; }

        public DateTime DateTimeLogged { get; set; }
    }

    public class ListAccessi
    {
        public static List<UsersLogged> users = new List<UsersLogged>();

        public static void PrintTableLoggedStory()
        {
            foreach (UsersLogged item in users)
            {
                Console.WriteLine($". Username {item.Username} {item.Surname}, Imposte dovute: {item.Imposta} " +
                                  $"accesso il {item.DateTimeLogged.ToShortDateString()}, alle ore {item.DateTimeLogged.ToShortTimeString()}");
            }
            Console.WriteLine("=========FINE LOG=========");
        }
    }
}