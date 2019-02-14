/*
	Copyright (c) 2019 Microsoft Corporation. All rights reserved. Licensed under the MIT license.
	See LICENSE in the project root for license information.
*/

using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Linq;
using console_csharp_connect_sample.Helpers;
using System.Collections.Generic;

namespace console_csharp_connect_sample
{
	// This static class returns a fully constructed 
	// instance of the GraphServiceClient with the client 
	// data to be used when authenticating requests to the Graph API
	public static class GraphClientFactory
	{		
		public static GraphServiceClient GetGraphServiceClient(string clientId, string authority, IEnumerable<string> scopes)
		{		
			var authenticationProvider = CreateAuthorizationProvider(clientId, authority, scopes);
			return new GraphServiceClient(authenticationProvider);
		}
		
		private static IAuthenticationProvider CreateAuthorizationProvider(string clientId, string authority, IEnumerable<string> scopes)
		{
			var clientApplication = new PublicClientApplication(clientId, authority);
			return new MsalAuthenticationProvider(clientApplication, scopes.ToArray());		
		}
	}
}
