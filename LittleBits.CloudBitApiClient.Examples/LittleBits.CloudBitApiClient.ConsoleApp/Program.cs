using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LittleBits.CloudBitApiClient.ConsoleApp
{
    class Program
    {
        #region constants

        const string MY_DEVICE_ID = "";
        const string MY_TOKEN = "";
        const string MY_TEST_API = "http://littlebitscallableapi.azurewebsites.net/api/sensor";

        #endregion

        static CloudBitApiClient.Client client = Client.Authenticate(MY_TOKEN);

        static void Main(string[] args)
        {
            // manipulating output
            Blink();

            // subscribing to the events on your cloudbit so your own code can handle it
            ListDevices();
            CreateSubscription();
            ListDevices();
            DeleteSubscription();
            ListDevices();

            // end it
            Console.WriteLine("--------");
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        static void Blink()
        {
            int i = 0;
            while (i < 3)
            {
                ActivateLed(); 
                Thread.Sleep(500);
                DeactivateLed();
                Thread.Sleep(500);
                i += 1;
            }
        }

        static void ActivateLed()
        {
            SetDeviceOutputLevel(100, -1);
        }

        static void DeactivateLed()
        {
            SetDeviceOutputLevel(0, -1);
        }

        static void SetDeviceOutputLevel(int level = 100, int duration = -1)
        {
            client.SetOutput(new DeviceOutputRequest
            {
                DeviceId = MY_DEVICE_ID,
                DurationInMilliseconds = duration,
                Percent = level
            });
        }

        static void ListDevices()
        {
            client.GetDeviceList(device =>
                {
                    Console.WriteLine("--------");
                    PropertyConsoleWriter.WriteDevice(device);
                });
        }

        static void CreateSubscription()
        {
            client.CreateSubscription(new CreateSubscriberRequest
            {
                PublisherId = MY_DEVICE_ID,
                SubscriberId = MY_TEST_API,
                PublisherEvents = new string[] { 
                        "amplitude:delta:ignite"
                    }
            });
        }

        static void DeleteSubscription()
        {
            client.DeleteSubscription(new PublisherSubscriberRequest
            {
                PublisherId = MY_DEVICE_ID,
                SubscriberId = MY_TEST_API
            });
        }
    }
}
