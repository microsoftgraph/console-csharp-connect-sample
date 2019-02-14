/*
	Copyright (c) 2019 Microsoft Corporation. All rights reserved. Licensed under the MIT license.
	See LICENSE in the project root for license information.
*/

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace console_csharp_connect_sample
{
	/// <summary>
	/// Description of the configuration of an AzureAD public client application (desktop/mobile application). This should
	/// match the application registration done in the Azure portal
	/// </summary>
	public class AuthenticationConfig
	{
		/// <summary>
		/// instance of Azure AD, for example public Azure or a Sovereign cloud (Azure China, Germany, US government, etc ...)
		/// </summary>
		public string Instance { get; set; } = "https://login.microsoftonline.com/{0}";
		
		/// <summary>
		/// The Tenant is:
		/// - either the tenant ID of the Azure AD tenant in which this application is registered (a guid)
		/// or a domain name associated with the tenant
		/// - or 'organizations' (for a multi-tenant application)
		/// </summary>
		public string TenantId { get; set; }
		
		/// <summary>
		/// Guid used by the application to uniquely identify itself to Azure AD
		/// </summary>
		public string ClientId { get; set; }

		/// <summary>
		/// Delegated resource access permissions as configured in the application registration in your Azure account portal
		/// </summary>
		public IEnumerable<string> Scopes { get; set; }
		
		/// <summary>
		/// URL of the authority
		/// </summary>
		public string Authority
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, Instance, TenantId);
			}
		}

		/// <summary>
		/// Checks whether the configuration parameters have values. 
		/// </summary>
		/// <exception cref="ArgumentNullException">
		/// Returns an ArgumentNullException if the parameter is empty or has white space
		/// </exception>
		public void CheckParameters()
		{
			string message = "Parameter missing value in the config file";
			if (string.IsNullOrWhiteSpace(ClientId))
				throw new ArgumentNullException(nameof(ClientId), message);
			if (string.IsNullOrWhiteSpace(TenantId))
				throw new ArgumentNullException(nameof(TenantId), message);
			if (string.IsNullOrWhiteSpace(Instance))
				throw new ArgumentNullException(nameof(Instance), message);
			if (!Scopes.Any())
				throw new ArgumentNullException(nameof(Scopes), message);								
		}

		/// <summary>
		/// Reads the configuration from a json file
		/// </summary>
		/// <param name="path">Path to the configuration json file</param>
		/// <returns>AuthenticationConfig read from the json file</returns>
		public static AuthenticationConfig ReadFromJsonFile(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException("Configuration file directory path not defined.");
			}
			try
			{
				IConfigurationRoot configuration;

				var builder = new ConfigurationBuilder()
				 .SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile(path,false);

				configuration = builder.Build();				
				return configuration.Get<AuthenticationConfig>();
			}
			catch (FileNotFoundException)
			{
				throw;
			}			
		}
	}
}
