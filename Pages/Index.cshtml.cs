using FrontierCodingTest.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace FrontierCodingTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<AccountModel> Accounts { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Accounts = GetAccounts();
        }

        private List<AccountModel> GetAccounts()
        {
            List<AccountModel> accounts = new List<AccountModel>();
            string myUrl = "https://frontiercodingtests.azurewebsites.net/api/accounts/getall";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(myUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    string responseString = responseContent.ReadAsStringAsync().Result;
                    accounts = JsonConvert.DeserializeObject<List<AccountModel>>(responseString);
                }
            }
                return accounts;
        }
    }
}
