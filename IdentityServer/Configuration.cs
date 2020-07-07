using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public static class Configuration
    {
        //idToken
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "rc.scope",
                    UserClaims =
                    {
                        "rc.garndma","rc.api.garndma"
                    }, 
                },
                   new IdentityResource
                {
                    Name = "samsung.user",
                    UserClaims =
                    {
                        "samsung.admin"
                    },
                }
           };
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
            //new ApiScope("ApiOne", "Read Access to API #1"),
            new ApiScope("ApiTwo", "Write Access to API #2"),
            new ApiScope("api1.read", "Read Access to API #1"),
            new ApiScope("api1.write", "Write Access to API #1")
            };
        }

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource(){
                Name="ApiOne",
                //Scopes = new List<string>{"ApiOne", "rc.scope" }
                Scopes = new List<string> {"api1.read", "api1.write"},
                },
                new ApiResource(){
                Name="ApiTwo",
                Scopes = new List<string>{ "ApiTwo" },
                UserClaims = new string[] { "rc.api.garndma" }
                },
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {

                new Client {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "ApiOne" }
                },

                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:44344/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44344/Home/Index" },

                    AllowedScopes = {
                        "api1.read",
                        "ApiTwo",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "rc.scope",
                    },

                    // puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,

                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
                 new Client {
                    ClientId = "samsung_id",
                    ClientSecrets = { new Secret("samsung_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    
                    RedirectUris = { "https://localhost:44343/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44343/Home/Index" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "samsung.user"
                    },


                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
            };
               
    }
}
