using RestSharp;
using RestSharp.Authenticators;
using System;
using TenmoClient.Models;

namespace TenmoClient
{
    public class ApiService
    {
        private readonly string API_URL = "";
        private readonly IRestClient client; //IRestClient?
        private static ApiUser user = new ApiUser();

        public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        public ApiService()
        {
            client = new RestClient();
        }

        public ApiService(IRestClient restClient)
        {
            client = restClient;
        }

        public ApiService(string api_url)
        {
            API_URL = api_url;
        }

        public decimal GetBalance(int userId)
        {
            RestRequest request = new RestRequest(API_URL + "accounts/" + userId + "/balance");
            IRestResponse<decimal> response = client.Get<decimal>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
            }
            else if (!response.IsSuccessful)
            {
                throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
            }
            else
            {
                return response.Data;
            }
        }
    }
}
