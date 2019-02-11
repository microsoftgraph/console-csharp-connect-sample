//Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.
using System;

namespace console_csharp_connect_sample
{
	class Program
    {			

		static void Main(string[] args)
        {	

			Console.WriteLine("Welcome to the C# Console Connect Sample!\n");			

			try
			{
				//*********************************************************************
				// setup Microsoft Graph Client for user.
				//*********************************************************************
				if (!string.IsNullOrWhiteSpace(Constants.ClientId) &&
					!string.IsNullOrWhiteSpace(Constants.Authority) &&
					!string.IsNullOrWhiteSpace(Constants.Scopes.ToString()))
				{
					var graphServiceClient = GraphClientFactory.GetGraphServiceClient(Constants.ClientId, Constants.Authority, Constants.Scopes);

					if (graphServiceClient != null)
					{
						bool sendMail = true;
						while (sendMail)
						{
							var user = graphServiceClient.Me.Request().GetAsync().Result;
							string userId = user.Id;
							string mailAddress = user.UserPrincipalName;
							string displayName = user.DisplayName;

							Console.WriteLine("Hello, " + displayName + ". Would you like to send an email to yourself or someone else?");
							Console.WriteLine("Enter the address to which you'd like to send a message. If you enter nothing, the message will go to your address.");
							string userInputAddress = Console.ReadLine();
							string messageAddress = String.IsNullOrEmpty(userInputAddress) ? mailAddress : userInputAddress;

							var mailHelper = new MailHelper(graphServiceClient);
							MailHelper.ComposeAndSendMailAsync("Welcome to Microsoft Graph development with C# and the Microsoft Graph Connect sample", Constants.EmailContent, messageAddress);

							Console.WriteLine("\nEmail sent! \n Want to send another message? Type 'y' for yes and any other key to exit.");
							ConsoleKeyInfo userInputSendMail = Console.ReadKey();
							sendMail = (userInputSendMail.KeyChar == 'y') ? true : false;
							Console.WriteLine();
						}
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
					Console.WriteLine("You haven't configured a value for either 'ClientId' or 'Authority' or 'Scopes' in Constants.cs. \nPlease follow the Readme instructions for configuring this application.");
					Console.ResetColor();
					Console.ReadKey();
					return;
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Sending an email failed with the following message: {0}", ex.Message);
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
