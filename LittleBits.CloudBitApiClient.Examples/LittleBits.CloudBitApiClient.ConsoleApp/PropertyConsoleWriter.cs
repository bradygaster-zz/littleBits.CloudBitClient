using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleBits.CloudBitApiClient.ConsoleApp
{
    public static class PropertyConsoleWriter
    {
        public static void WriteDevice(Device d)
        {
            foreach (var propInfo in typeof(Device).GetProperties())
            {
                if (propInfo.PropertyType != typeof(DeviceSubscriber[]))
                {
                    Console.WriteLine(string.Format("{0}: {1}", propInfo.Name, propInfo.GetValue(d)));
                }
                else
                {
                    var subscribers = (DeviceSubscriber[])propInfo.GetValue(d);

                    if(subscribers == null || subscribers.Length == 0)
                        Console.WriteLine("No subscribers");

                    foreach (var subscriber in subscribers)
                    {
                        Console.WriteLine(string.Format("{0} subscribed to device {1}",
                            subscriber.SubscriberId, subscriber.PublisherId));
                    }
                }
            }
        }
    }
}
