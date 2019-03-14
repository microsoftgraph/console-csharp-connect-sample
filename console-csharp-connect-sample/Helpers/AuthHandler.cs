/*
	Copyright (c) 2019 Microsoft Corporation. All rights reserved. Licensed under the MIT license.
	See LICENSE in the project root for license information.
*/

using Microsoft.Graph;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace console_csharp_connect_sample.Helpers
{
	/// <summary>
	/// This class allows an implementation of IAuthenticationProvider to be inserted into the DelegatingHandler
	/// pipeline of an HttpClient instance. In future versions of GraphSDK, many cross-cutting concerns will
	/// be implemented as DelegatingHandlers. This AuthHandler will come in the box.
	/// </summary>
	public class AuthHandler : DelegatingHandler
	{
		private IAuthenticationProvider _authenticationProvider;

		public AuthHandler(IAuthenticationProvider authenticationProvider, HttpMessageHandler innerHandler)
		{
			InnerHandler = innerHandler;
			_authenticationProvider = authenticationProvider;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
		{
			await _authenticationProvider.AuthenticateRequestAsync(request);
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
