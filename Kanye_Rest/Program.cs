using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace Kanye_Rest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Client/URL
            var client = new HttpClient();
            var kanyeURL = "https://api.kanye.rest/";
            var ronURL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
            #endregion

            var cont = false;

            do
            {
                #region UserInput
                Console.WriteLine("Kanye West and Ron Swanson Conversation simulator.");
                Console.WriteLine("");
                Console.WriteLine("Please enter the number of exchanges you would like to see:");
                Console.WriteLine("");
                int exchanges;
                bool condition;
                do
                {
                    condition = int.TryParse(Console.ReadLine(), out exchanges);
                } while (!condition);
                Console.WriteLine("");
                Console.WriteLine("Conversation shall now be simulated.");
                Console.WriteLine("Enter any key to continue:");
                Console.ReadLine();
                Console.Clear();
                #endregion


                #region Kanye West Quote Generator
                var kanyeSays = new string[exchanges];
                for (int i = 0; i < kanyeSays.Length; i++)
                {
                        kanyeSays[i] = JObject.Parse(client.GetStringAsync(kanyeURL)
                            .Result).GetValue("quote").ToString();
                }
                #endregion

                #region Ron Swanson Quote Generator
                var ronSays = new string[exchanges];
                for (int i = 0; i < ronSays.Length; i++)
                {
                    ronSays[i] = JArray.Parse(client.GetStringAsync(ronURL)
                        .Result).ToString().Replace('[', ' ').Replace(']', ' ').Trim();
                }
                #endregion

                #region Conversation
                for (int i = 0; i < kanyeSays.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Kayne says, \"{kanyeSays[i]}.\"");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Ron says, {ronSays[i]}");
                }
                #endregion

                #region User Continue
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");
                Console.WriteLine("Enter any key to continue:");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Would you like to simulate another conversation?");
                Console.WriteLine("Y/N");
                var answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "y":
                    case "yes":
                    case "yup":
                    case "okay":
                    case "ok":
                    case "sure":
                    case "affirmative":
                    case "yea":
                    case "yeah":
                    case "true":
                        cont = true;
                        break;
                    default: 
                        cont = false; 
                        break;
                }
                #endregion

                Console.Clear();

            } while (cont == true);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}