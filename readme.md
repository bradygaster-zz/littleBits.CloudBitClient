## littleBits cloudBit API Client Library for .NET ##

This repository contains code you can use from [almost] any .NET platform, from Windows Desktop to Windows Store and Phone applications, to control a littleBits cloudBit module. The client also supports creating and deleting subscribers for cloudBit sensor changes, so you can literally write your own API and teach your cloudBit to call it when things happen. 

The goal for the cloudBit API Client Library for .NET is to provide client developers a smooth and consistent way of writing code against the littleBits Cloud HTTP API, but to do so in a client library the API of which would be easy to use and inspire extension. 

## Required Knowledge ##

If you're new to littleBits, you'll want to: 

- [Check out the littleBits web site](http://littlebits.cc)
- [Specifically check out the littleBits cloudBit module page](http://littlebits.cc/bits/cloudbit)
- [Read the documentation on the littleBits Cloud HTTP API](http://developer.littlebitscloud.cc/api-http)
- [Install the NuGet package](https://www.nuget.org/packages/LittleBits.CloudBitApiClient)

## Using the Client Library ##

You can either fork and clone this repository and compile the code yourself, or you can grab it from NuGet (see the link below). The following features are provided in this client library:

- Retrieve a list of cloudBit devices registered with the littleBits cloud
- View publishers and subscribers of each cloudBit device
- Add and delete subscribers to/from each cloudBit device

## Code Examples ##
To get started with a Client in code that can be used against the littleBits API, you'll need access to the littleBits Electronics Cloud Control panel. Access to it is a benefit of the cloudBit module, so you can manage your bits from the cloud. 

	// you'll need to have your own values or read them from config
	const string MY_DEVICE_ID = "";
    const string MY_TOKEN = "";
    const string MY_TEST_API = "http://littlebitscallableapi.azurewebsites.net/api/sensor";

#### Setting an output module's value ####
In the case that your cloudBit is connected to  a servo or an LED, and you want to send it either an on/off or 1-100 range, you can do that using the Client class. 

	client.SetOutput(new DeviceOutputRequest
	{
	    DeviceId = MY_DEVICE_ID,
	    DurationInMilliseconds = duration, // setting to -1 results in an infinite ON
	    Percent = 100
	});

## Publishing & Subscribing ##
You can subscribe to any of your cloudBit devices. When enabled with a button module or sensor module, the voltage created causes the sensor to trip, and the signal is then sent to the littleBits Cloud HTTP API. 
 
### Creating Subscriptions ###
Subscriptions are external URIs or other cloudBits that can engage in conversation wire

	client.CreateSubscription(new CreateSubscriberRequest
	{
	    PublisherId = MY_DEVICE_ID,
	    SubscriberId = MY_TEST_API,
	    PublisherEvents = new string[] { 
	            "amplitude:delta:ignite"
	        }
	});

### Deleting Subscriptions ###
The code below will remove subscriptions. 

	client.DeleteSubscription(new PublisherSubscriberRequest
    {
        PublisherId = MY_DEVICE_ID,
        SubscriberId = MY_TEST_API
    });

NOTE: When you remove a subscription you're effectively telling the cloudBit you want it to stop calling your own custom API each time it gets a message from the cloudBit. 