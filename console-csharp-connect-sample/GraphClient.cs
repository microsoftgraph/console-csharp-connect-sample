using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Linq;
using console_csharp_connect_sample.Helpers;

namespace console_csharp_connect_sample
{
	// This class holds the graph client data and 
	// returns a fully constructed instance of the 
	// GraphServiceClient with the client data to be 
	// used when authenticating requests to the Graph API
	public class GraphClient
	{
		private GraphServiceClient ServiceClient;
		private string ClientId { get; set; }
		private string Authority { get; set; }
		private string[] Scopes { get; set; }

		public GraphClient(string clientId, string authority, string[] scopes)
		{
			ClientId = clientId;
			Authority = authority;
			Scopes = scopes;			
		}
				
		public GraphServiceClient GetGraphServiceClient()
		{		
			var authenticationProvider = CreateAuthorizationProvider();
			ServiceClient = new GraphServiceClient(authenticationProvider);
			return ServiceClient;
		}
		
		private IAuthenticationProvider CreateAuthorizationProvider()
		{
			var clientApplication = new PublicClientApplication(ClientId, Authority);
			return new MsalAuthenticationProvider(clientApplication, Scopes.ToArray());		
		}
	}
}
