using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BloodBankMgmt.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BloodBankMgmt.Controllers
{
    public class DonorProjController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            Userpass obj = new Userpass { Usern = "admin", Pass = "admin" };
            using (HttpClient client = new HttpClient())
            {
                var token = GetToken("https://localhost:44326/api/Token", obj);
                client.BaseAddress = new Uri("https://localhost:44375/api/");
                // MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                // client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = client.GetAsync("Donors").Result;

                string stringData = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<IEnumerable<Donor>>(stringData);

                return View(data);

            }

        }

        static string GetToken(string url, Userpass user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, data).Result;
                string name = response.Content.ReadAsStringAsync().Result;
                dynamic details = JObject.Parse(name);
                return details.token;
            }
        }

    }

}
