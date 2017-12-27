//Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.
using Microsoft.Graph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace console_csharp_connect_sample
{
    class Program
    {
        public static GraphServiceClient graphClient;
        
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the C# Console Connect Sample!\n");

            try
            {
                //*********************************************************************
                // setup Microsoft Graph Client for user.
                //*********************************************************************
                if (Constants.ClientId != "ENTER_YOUR_CLIENT_ID")
                {
                    graphClient = AuthenticationHelper.GetAuthenticatedClient();
                    if (graphClient != null)
                    {
                        var user = graphClient.Me.Request().GetAsync().Result;
                        string userId = user.Id;
                        string mailAddress = user.UserPrincipalName;
                        string displayName = user.DisplayName;
                        
                        Console.WriteLine("Hello, " + displayName + ". Would you like to get your trending information?");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        Console.WriteLine();

                        // TODO: Enter code to access trending information.
     
                        Console.WriteLine("\n\nPress any key to continue.");
                        Console.ReadKey();


                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("We weren't able to create a GraphServiceClient for you. Please check the output for errors.");
                        Console.ResetColor();
                        Console.ReadKey();
                        return;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You haven't configured a value for ClientId in Constants.cs. Please follow the Readme instructions for configuring this application.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Getting your trending information failed with the following message: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
        }
    }
}
