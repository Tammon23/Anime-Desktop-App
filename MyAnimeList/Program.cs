using System;
using System.Threading.Tasks;

namespace MyAnimeList
{
    public class Program
    {
        static async Task Main()
        {
            // initializing variables for first time run
            OAuth.Init();
            OAuth.RefreshToken();
            
            // getting the allow/cancel link (click it)
            /*Console.WriteLine(OAuth.GetUserAuthUrl());
            
            string AutorisationURL;
            while ((AutorisationURL = Console.ReadLine()) != null)
            {
                AutorisationURL = AutorisationURL.Trim();
                await OAuth.InitUserAccessToken(AutorisationURL);
                break;
            }*/
            
            Console.ReadKey();
        }
    }
}