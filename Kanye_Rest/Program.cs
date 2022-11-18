using Newtonsoft.Json.Linq;

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

            #region Kanye West Quote Generator
            var kanyeQuotes = new string[5];
            var kanyeResponses = new string[5];
            for (int i = 0; i < kanyeResponses.Length; i++)
            { 
                kanyeResponses[i] = client.GetStringAsync(kanyeURL).Result;
                kanyeQuotes[i] = JObject.Parse(kanyeResponses[i]).GetValue("quote").ToString();
            }
            #endregion

            #region Ron Swanson Quote Generator
            var ronQuotes = new string[5];
            var ronResponses = new string[5];
            for (int i = 0; i < ronResponses.Length; i++)
            { 
                ronResponses[i] = client.GetStringAsync(ronURL).Result;
                ronQuotes[i] = JArray.Parse(ronResponses[i]).ToString().Replace('[', ' ').Replace(']', ' ').Trim();
            }
            #endregion

            #region Conversation
            for (int i = 0; i < kanyeResponses.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Kayne says, \"{kanyeQuotes[i]}.\"");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"Ron says, {ronQuotes[i]}");
            }
            #endregion

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
    }
}