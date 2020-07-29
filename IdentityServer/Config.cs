// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4.Test;

namespace IdentityServer4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiScope> GetApis()
        {
            return new ApiScope[]
            {
                new ApiScope("api1", "API 1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "consoleClient",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },

                 // OpenID Connect Code Grant Type ASP.NET Core MVC App
                new Client
                {
                    ClientId = "mvcWebApp",
                    ClientName = "MVC Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "http://localhost:58791/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:58791/signout-callback-oidc" },

                    RequireConsent = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                }
           };
        }
    }
}