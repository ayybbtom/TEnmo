using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient
{
    public class ApiService
    {
        private readonly string API_URL = "";
        private readonly RestClient client = new RestClient();  
        private static ApiUser user = new ApiUser();


        public bool LoggedIn { get { return !string.IsNullOrWhiteSpace(user.Token); } }

        public ApiService(string api_url)
        {
            API_URL = api_url;
        }

        public decimal GetBalance(int userId)
        {
            RestRequest request = new RestRequest(API_URL + "accounts/" + userId + "/balance"); //header
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
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

        public List<Transfer> TransferLookupUserId(int userId)
        {
            // + userId + "/alltransfers"
            RestRequest request = new RestRequest(API_URL + "transfer/" + userId + "/alltransfers");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

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
