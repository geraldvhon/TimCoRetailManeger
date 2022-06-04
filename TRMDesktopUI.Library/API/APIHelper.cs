using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.API;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.API
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient _apiClient;
        private ILoggedInUserModel _loggedInUser;

        public APIHelper(ILoggedInUserModel loggedInUser)
        {
            InizializeClient();
            _loggedInUser = loggedInUser;
        }

        private void InizializeClient() 
        {
            //string api = ConfigurationManager.AppSettings["api"];
            string api = "https://localhost:44349/";
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient ApiClient 
        { 
            get
            {
                return _apiClient;
            }

        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair <string, string >("grant_type", "password"),
                new KeyValuePair <string, string >("username", username),
                new KeyValuePair <string, string >("password", password)
            });

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public void LogOffUser()
        {
            _apiClient.DefaultRequestHeaders.Clear();
        }



        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.DateCreated = result.DateCreated;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.Firstname = result.Firstname;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.Lastname = result.Firstname;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }





    }
}
