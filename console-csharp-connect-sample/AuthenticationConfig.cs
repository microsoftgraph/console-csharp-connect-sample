using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;



namespace console_csharp_connect_sample
{
	public class AuthenticationConfig
	{
		public string Instance { get; set; } = "https://login.microsoftonline.com/{0}";
		public string TenantId { get; set; }
		public string ClientId { get; set; }
		public IEnumerable<string> Scopes { get; set; }
		public string Authority
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, Instance, TenantId);
			}
		}			

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
				.AddJsonFile(path);

				configuration = builder.Build();
				return configuration.Get<AuthenticationConfig>();
			}
			catch (FileNotFoundException ex)
			{
				throw new FileNotFoundException(ex.Message);
			}			
		}
	}
}
