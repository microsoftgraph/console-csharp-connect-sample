/*
	Copyright (c) 2019 Microsoft Corporation. All rights reserved. Licensed under the MIT license.
	See LICENSE in the project root for license information.
*/

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;
using System.Linq;

namespace console_csharp_connect_sample.Helpers
{
	/// <summary>
	/// This class encapsulates the details of getting a token from MSAL and exposes it via the 
	/// IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.
	/// </summary>
	/// A significantly enhanced version of this class will in the future be available from
	/// the GraphSDK team. It will support all the types of Client Application as defined by MSAL.
	public class MsalAuthenticationProvider : IAuthenticationProvider
	{		
		private PublicClientApplication _clientApplication;
		private  string[] _scopes;
	    
		public MsalAuthenticationProvider(PublicClientApplication clientApplication, string[] scopes)
		{
			_clientApplication = clientApplication;
			_scopes = scopes;
		}

		/// <summary>
		/// Update HttpRequestMessage with credentials
		/// </summary>
		public async Task AuthenticateRequestAsync(HttpRequestMessage request)
		{
			var authentication = await GetAuthenticationAsync();
			request.Headers.Authorization = AuthenticationHeaderValue.Parse(authentication.CreateAuthorizationHeader());
		}

		/// <summary>
		/// Acquire Token for user
		/// </summary>
		public async Task<AuthenticationResult> GetAuthenticationAsync()
		{
			AuthenticationResult authResult = null;
			var accounts = await _clientApplication.GetAccountsAsync();

			try
			{
				authResult = await _clientApplication.AcquireTokenSilentAsync(_scopes, accounts.FirstOrDefault());
			}
			catch (MsalUiRequiredException)
			{
				try
				{
					authResult = await _clientApplication.AcquireTokenAsync(_scopes);					
				}
				catch (MsalException)
				{
					throw;
				}
			}

			return authResult;
		}

	}
}
