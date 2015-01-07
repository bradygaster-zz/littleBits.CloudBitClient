using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace LittleBits.CloudBitApiClient
{
    public class Client
    {
        const string V2_VERSION = "v2";
        const string URL_TEMPLATE_DEVICE_LIST = "https://api-http.littlebitscloud.cc/{0}/devices";
        const string URL_TEMPLATRE_DEVICE_OUTPUT = "https://api-http.littlebitscloud.cc/{0}/devices/{1}/output";
        const string URL_TEMPLATE_SUBSCRIPTIONS = "https://api-http.littlebitscloud.cc/subscriptions";

        private string _token;

        public static Client Authenticate(string bearerToken)
        {
            return new Client(bearerToken);
        }

        private Client(string bearerToken)
        {
            this._token = bearerToken;
        }

        private HttpClient SetupHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("BEARER {0}", this._token));
            return client;
        }

        public Client GetDeviceList(Action<Device> actionToRunOnDevice)
        {
            return GetDeviceList(actionToRunOnDevice, null);
        }

        public Client GetDeviceList(Action<Device> actionToRunOnDevice, Action actionToRunBetweenDevices)
        {
            var client = SetupHttpClient();
            var result = client.GetStringAsync(
                string.Format(URL_TEMPLATE_DEVICE_LIST, V2_VERSION)
                ).Result;

            var devices = JsonConvert.DeserializeObject<Device[]>(result);

            foreach (var device in devices)
            {
                if (actionToRunOnDevice != null)
                    actionToRunOnDevice(device);

                if (actionToRunBetweenDevices != null)
                    actionToRunOnDevice(device);
            }

            return this;
        }

        public Client SetOutput(DeviceOutputRequest outputRequest)
        {
            var client = SetupHttpClient();
            var url = string.Format(URL_TEMPLATRE_DEVICE_OUTPUT, V2_VERSION, outputRequest.DeviceId);
            client.PostAsync<DeviceOutputRequest>(url, outputRequest, new JsonMediaTypeFormatter()).Wait();
            return this;
        }

        public Client DeleteSubscription(PublisherSubscriberRequest request)
        {
            var client = SetupHttpClient();
            var url = string.Format(URL_TEMPLATE_SUBSCRIPTIONS, V2_VERSION);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, url);
            var data = new Dictionary<string, object> {
                {"publisher_id", request.PublisherId},
                {"subscriber_id", request.SubscriberId}
            };
            httpRequest.Content = new ObjectContent<IDictionary<string, object>>(data, new JsonMediaTypeFormatter());
            var result = client.SendAsync(httpRequest).Result;
            return this;
        }

        public Client CreateSubscription(CreateSubscriberRequest request)
        {
            var client = SetupHttpClient();
            var url = string.Format(URL_TEMPLATE_SUBSCRIPTIONS);
            var result = client.PostAsync<CreateSubscriberRequest>(url, request, new JsonMediaTypeFormatter()).Result;
            return this;
        }
    }
}
