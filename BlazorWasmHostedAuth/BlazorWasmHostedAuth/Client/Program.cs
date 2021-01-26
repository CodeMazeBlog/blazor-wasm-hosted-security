using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWasmHostedAuth.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("BlazorWasmHostedAuth.ServerAPI", 
				client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			builder.Services.AddScoped(sp => 
				sp.GetRequiredService<IHttpClientFactory>()
				.CreateClient("BlazorWasmHostedAuth.ServerAPI"));

			builder.Services.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			await builder.Build().RunAsync();
		}
	}
}
