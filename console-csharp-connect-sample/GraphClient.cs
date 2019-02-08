using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Linq;
using console_csharp_connect_sample.Helpers;

namespace console_csharp_connect_sample
{
	public class GraphClient
	{
		private GraphServiceClient Client { get; set; }
		private string ClientId { get; set; }
		private string Authority { get; set; }
		private string[] Scopes { get; set; }

		public GraphClient(string clientId, string authority, string[] scopes)
		{
			ClientId = clientId;
			Authority = authority;
			Scopes = scopes;			
		}
				
		public GraphServiceClient GetGraphClient()
		{		
			var authenticationProvider = CreateAuthorizationProvider();
			Client = new GraphServiceClient(authenticationProvider);
			return Client;
		}
		
		private IAuthenticationProvider CreateAuthorizationProvider()
		{
			var clientApplication = new PublicClientApplication(ClientId, Authority);
			return new MsalAuthenticationProvider(clientApplication, Scopes.ToArray());		
		}
	}
}
