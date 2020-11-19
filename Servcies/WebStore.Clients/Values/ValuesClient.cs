using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

using WebStore.Clients.Base;
using WebStore.Interfaces.TestApi;

namespace WebStore.Clients.Values
{
    public class ValuesClient : BaseClient, IValueService
    {
        public ValuesClient(IConfiguration Configuration) : base(Configuration, "api/v1/values") { }

        public IEnumerable<string> Get()
        {
            var respone = _Client.GetAsync(_ServiceAddress).Result;
            if (respone.IsSuccessStatusCode)
                return respone.Content.ReadAsAsync<IEnumerable<string>>().Result;

            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var respone = _Client.GetAsync($"{_ServiceAddress}/{id}").Result;
            if (respone.IsSuccessStatusCode)
                return respone.Content.ReadAsAsync<string>().Result;

            return string.Empty;
        }

        public Uri Post(string value)
        {
            var response = _Client.PostAsJsonAsync(_ServiceAddress, value).Result;
            return response.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var response = _Client.PutAsJsonAsync($"{_ServiceAddress}/{id}", value).Result;
            return response.EnsureSuccessStatusCode().StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var response = _Client.DeleteAsync($"{_ServiceAddress}/{id}").Result;
            return response.StatusCode;
        }
    }
}
