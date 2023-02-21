﻿using Newtonsoft.Json.Linq;

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

            #region UserInput
            Console.WriteLine("Kanye West and Ron Swanson Conversation simulator.");
            Console.WriteLine("");
            Console.WriteLine("Please enter the number of exchanges you would like to see:");
            Console.WriteLine("");
            var exchanges = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            Console.WriteLine("Conversation shall now be simulated.");
            Console.WriteLine("Enter any key to continue:");
            Console.ReadLine();
            Console.Clear();
            #endregion


            #region Kanye West Quote Generator
            var kanyeQuotes = new string[exchanges];
            var kanyeResponses = new string[exchanges];
            for (int i = 0; i < kanyeResponses.Length; i++)
            { 
                kanyeResponses[i] = client.GetStringAsync(kanyeURL).Result;
                kanyeQuotes[i] = JObject.Parse(kanyeResponses[i]).GetValue("quote").ToString();
            }
            #endregion

            #region Ron Swanson Quote Generator
            var ronQuotes = new string[exchanges];
            var ronResponses = new string[exchanges];
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