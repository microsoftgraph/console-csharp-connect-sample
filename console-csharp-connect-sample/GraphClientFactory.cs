using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Linq;
using console_csharp_connect_sample.Helpers;

namespace console_csharp_connect_sample
{
	// This static class returns a fully constructed 
	// instance of the GraphServiceClient with the client 
	// data to be used when authenticating requests to the Graph API
	public static class GraphClientFactory
	{
		private static GraphServiceClient _serviceClient;

		public static GraphServiceClient GetGraphServiceClient(string clientId, string authority, string[] scopes)
		{		
			var authenticationProvider = CreateAuthorizationProvider(clientId, authority, scopes);
			_serviceClient = new GraphServiceClient(authenticationProvider);
			return _serviceClient;
		}
		
		private static IAuthenticationProvider CreateAuthorizationProvider(string clientId, string authority, string[] scopes)
		{
			var clientApplication = new PublicClientApplication(clientId, authority);
			return new MsalAuthenticationProvider(clientApplication, scopes.ToArray());		
		}
	}
}
