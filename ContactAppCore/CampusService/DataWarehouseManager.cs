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

        public DataWarehouseItem GetDataWarehouseItem(string netid) {
            if (string.IsNullOrEmpty(netid)) {
                return new DataWarehouseItem();
            }
            //TODO Remove try/catch block when we enforce VPN / Campus connection
            try {
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
                    FirstName = information?.name?.firstName?.ToString() ?? string.Empty,
                    LastName = information?.name?.lastName?.ToString() ?? string.Empty,
                    Title = information?.title ?? string.Empty,
                    AddressLine1 = information?.address?.streetLine1?.ToString() ?? string.Empty,
                    AddressLine2 = information?.address?.streetLine2?.ToString() ?? string.Empty,
                    City = information?.address?.city?.ToString() ?? string.Empty,
                    State = information?.address?.state?.code?.ToString() ?? string.Empty,
                    ZipCode = information?.address?.zipCode?.ToString() ?? string.Empty,
                    Phone = information?.phone?.phoneNumber?.ToString() ?? string.Empty,
                    PhoneAreaCode = information?.phone?.areaCode?.ToString() ?? string.Empty
                };
            } catch {
                return new DataWarehouseItem();
            }
        }
    }
}