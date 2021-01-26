using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWasmHostedAuth.Client
{
	public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
	{
		public CustomUserFactory(IAccessTokenProviderAccessor accessor)
			: base(accessor)
		{
		}

		public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
			RemoteUserAccount account,
			RemoteAuthenticationUserOptions options)
		{
			var user = await base.CreateUserAsync(account, options);
			var claimsIdentity = (ClaimsIdentity)user.Identity;

			if (account != null)
			{
				MapArrayClaimsToMultipleSeparateClaims(account, claimsIdentity);
			}

			return user;
		}

		private void MapArrayClaimsToMultipleSeparateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
		{
			foreach (var prop in account.AdditionalProperties)
			{
				var key = prop.Key;
				var value = prop.Value;
				if (value != null &&
					(value is JsonElement element && element.ValueKind == JsonValueKind.Array))
				{
					claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(prop.Key));
					var claims = element.EnumerateArray()
						.Select(x => new Claim(prop.Key, x.ToString()));
					claimsIdentity.AddClaims(claims);
				}
			}
		}
	}
}
