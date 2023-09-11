using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace ContactAppCore.CampusService {

    public class DataWarehouseManager {
        private readonly string _baseUrl = "";
        private readonly string _key = "";

        public DataWarehouseManager() {
        }

        public DataWarehouseManager(string baseUrl, string key) {
            _baseUrl = baseUrl;
            _key = key;
        }

        public DataWarehouseItem GetFirstAndLastName(string netid) {
            var url = _baseUrl + "/directory-person/person-lookup-query/" + netid;
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);
            using var res = client.GetAsync(url).Result;
            if (!res.IsSuccessStatusCode) {
                throw new Exception("Error accessing directory-person query: " + res.StatusCode);
            }
            using var content = res.Content;
            var json = content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            var information = data.list[0];
            return new DataWarehouseItem() {
                FirstName = information?.name?.firstName?.ToString() ?? "",
                LastName = information?.name?.lastName?.ToString() ?? "",
                Title = information?.title ?? ""
            };
        }
    }
}