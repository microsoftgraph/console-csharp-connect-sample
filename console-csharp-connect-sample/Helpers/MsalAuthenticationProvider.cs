using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Graph;

namespace console_csharp_connect_sample.Helpers
{
	// This class encapsulates the details of getting a token from MSAL and exposes it via the 
	// IAuthenticationProvider interface so that GraphServiceClient or AuthHandler can use it.
	// A significantly enhanced version of this class will in the future be available from
	// the GraphSDK team. It will support all the types of Client Application as defined by MSAL.
	public class MsalAuthenticationProvider : IAuthenticationProvider
	{		
		private PublicClientApplication _clientApplication;
		private  string[] _scopes;
	    private string _userToken = null;
		private DateTimeOffset _expiration;

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
			var token = await GetTokenAsync();
			request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
		}

		/// <summary>
		/// Acquire Token for user
		/// </summary>
		public async Task<string> GetTokenAsync()
		{		
			if (_userToken == null || _expiration <= DateTimeOffset.UtcNow.AddMinutes(5))
			{
				AuthenticationResult authResult = null;
				authResult = await _clientApplication.AcquireTokenAsync(_scopes);
				_userToken = authResult.AccessToken;
				_expiration = authResult.ExpiresOn;
			}
			return _userToken;
		}

	}
}
