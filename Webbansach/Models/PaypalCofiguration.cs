using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace Webbansach.Models
{
    public class PaypalCofiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PaypalCofiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();

        }

        public static string GetAccessToken()
        {
            string acessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return acessToken;
        }


        public static APIContext GetAPIContext()
        {
            APIContext aPIContext = new APIContext(GetAccessToken());
            aPIContext.Config = GetConfig();
            return aPIContext;
        }
    }
}