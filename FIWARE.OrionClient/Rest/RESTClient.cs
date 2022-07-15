using FIWARE.OrionClient.IoTAgent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.OrionClient.REST
{
    public class RESTClient<T>
    {
        private string AuthHeaderKey;
        private string AuthToken;

        public RESTClient()
        {

        }

        /// <summary>
        /// Creates a new instance of the RESTClient with authentication information
        /// </summary>
        /// <param name="authHeaderKey"></param>
        /// <param name="authToken"></param>
        public RESTClient(string authHeaderKey, string authToken)
        {
            this.AuthHeaderKey = authHeaderKey;
            this.AuthToken = authToken;
        }

        /// <summary>
        /// Retrieves the date from the provided URI and returns it as an object of type T
        /// </summary>
        /// <param name="uri">The URL to retrieve</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Posts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to post to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PostAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Posts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to post to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<string> PostAsyncTriggerSensorBackDoor(string direction)
        {
            if (!direction.Equals("UP") && !direction.Equals("DOWN"))
                return string.Empty;

            string url = "http://localhost:56000/api/SensorBackDoor";
            string strParameter = string.Format("?direction={0}", direction);
            var client = new HttpClient();

            HttpContent postContent = new StringContent("");

            var response = await client.PostAsync((url + strParameter), postContent);

            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public async Task<string> PatchAsyncTriggerExhaustorCommand(string command)
        {
            string url = "http://localhost:1026/v2/entities/urn:ngsi-ld:fanEhxaustor:001/attrs";
            var httpClient = new HttpClient();
            HttpResponseMessage response;
            try
            {
                string body;

                if (command.Equals("on"))
                    body = "{\"on\":{\"type\":\"command\",\"value\":\"\"}}";
                else if (command.Equals("off"))
                    body = "{\"off\":{\"type\":\"command\",\"value\":\"\"}}";
                else
                    return "";

                var data = new StringContent(body, Encoding.UTF8, "application/json");                
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);

                request.Headers.Add("fiware-service", "openiot");
                request.Headers.Add("fiware-servicepath", "/");

                request.Content = new StringContent(body, System.Text.Encoding.UTF8, "application/json");
                response = await httpClient.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public async Task<Object> GetMeasurementFromBroker(string entityName, string thingType)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("http://localhost:1026/v2/entities/{0}", entityName);


                client.DefaultRequestHeaders.Add("fiware-service", "openiot");
                client.DefaultRequestHeaders.Add("fiware-servicepath", "/");

                HttpResponseMessage clientResponse = await client.GetAsync(url);

                string result = clientResponse.Content.ReadAsStringAsync().Result;

                RootDataCatcher thing = new RootDataCatcher();

                if (thingType.Equals("sensor"))
                    thing = JsonConvert.DeserializeObject<SensorRootDataCatcher>(result) as SensorRootDataCatcher;
                else if (thingType.Equals("exhaustor"))
                    thing = JsonConvert.DeserializeObject<ExhaustorRootDataCatcher>(result) as ExhaustorRootDataCatcher;

                return thing;
            }
        }

        /// <summary>
        /// Puts the data to the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to put to</param>
        /// <param name="body">The body content</param>
        /// <returns></returns>
        public async Task<T> PutAsync(string uri, string body)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(uri, postContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Deletes the date at the provided URI and returns the response as an object of type T
        /// </summary>
        /// <param name="uri">The URI to delete</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(string uri)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(AuthHeaderKey) && !string.IsNullOrWhiteSpace(AuthToken))
                    client.DefaultRequestHeaders.Add(AuthHeaderKey, AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    T genericResponse = JsonConvert.DeserializeObject<T>(content);

                    return genericResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
