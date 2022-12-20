using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Xml.Serialization;
using Using_API.Models;

namespace Using_API.HttpClientServices
{
    public class HTTPService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly Uri _url;

        public HTTPService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("Movie");
            _url = _httpClient.BaseAddress;
        }

        public async Task<List<T>> GetAll<T>()
        {
            var response = await _httpClient.GetAsync(_url + "/Movies");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = new List<T>();

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<List<T>>(content);
            }
            else if (response.Content.Headers.ContentType.MediaType == "application/xml")
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                result = (List<T>)serializer.Deserialize(new StringReader(content));
            }

            return result;
        }

        public async Task<T> GetById<T>(int id)
        {
            var response =await _httpClient.GetAsync(_url + "/Movies/" + id);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var result = (T)Activator.CreateInstance(typeof(T));

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else if (response.Content.Headers.ContentType.MediaType == "application/xml")
            {
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(new StringReader(content));
            }

            return result;
        }

        public async Task<T> Create<T>(T item)
        {
            
            var itemJson = new StringContent(
                   JsonConvert.SerializeObject(item),
                   Encoding.UTF8,
                   "application/json");
            var response = await _httpClient.PostAsync(
                _url + "/Movies",
                itemJson);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var createdItem = JsonConvert.DeserializeObject<T>(content);

            return createdItem;
        }

        public async Task<T> Update<T>(T item, int id)
        {
            var itemJson = new StringContent(
                JsonConvert.SerializeObject(item),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PutAsync(_url + "/Movies/"+ id, itemJson);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var result = (T)Activator.CreateInstance(typeof(T));
            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else if (response.Content.Headers.ContentType.MediaType == "application/xml")
            {
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(new StringReader(content));
            }

            return result;
        }

        public async Task<T> Delete<T>(int id)
        {
            var response = await _httpClient.DeleteAsync(_url + "/Movies/" + id);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var result = (T)Activator.CreateInstance(typeof(T));

            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                result = JsonConvert.DeserializeObject<T>(content);
            }
            else if (response.Content.Headers.ContentType.MediaType == "application/xml")
            {
                var serializer = new XmlSerializer(typeof(T));
                result = (T)serializer.Deserialize(new StringReader(content));
            }

            return result;
        }

    }
}
