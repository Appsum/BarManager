using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Waitress.Configuration;
using Waitress.Events;

namespace Waitress
{
    public class BusListener : BackgroundService
    {
        private readonly IServiceProvider _services;

        public BusListener(IServiceProvider services)
        {
            _services = services;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Get the DI ServiceProvider in the BackgroundService
            using IServiceScope scope = _services.CreateScope();
            IServiceProvider serviceProvider = scope.ServiceProvider;

            // Load the dependencies through the ServiceProvider. In classes after the ServiceCollection has been built, this can be done through ctor injection instead
            ServiceBusSettings serviceBusSettings = serviceProvider.GetRequiredService<IOptions<ServiceBusSettings>>().Value;
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // Configure the SubscriptionClient with the settings and make it put a lock on the message when it is being read.
            // This stops other subscriptions from handling the same message, which enables this service to be scaled well.
            ISubscriptionClient subscriptionClient = new SubscriptionClient(serviceBusSettings.ConnectionString,
                                                                            serviceBusSettings.TopicName,
                                                                            serviceBusSettings.SubscriptionName,
                                                                            ReceiveMode.PeekLock,
                                                                            RetryPolicy.Default);

            // Register a Func that will be called when a new message has been received on the SubscriptionClient (with its configured SubscriptionName and TopicName)
            subscriptionClient.RegisterMessageHandler(async (message, cancellationToken) =>
            {
                try
                {
                    string rawJsonString = Encoding.UTF8.GetString(message.Body);
                    var orderPlaced = JsonSerializer.Deserialize<OrderPlaced>(rawJsonString);
                    if (orderPlaced == null)
                    {
                        throw new NullReferenceException("The received OrderPlaced message is empty.");
                    }
                    await mediator.Publish(new OrderPlacedMessageReceived(orderPlaced.Order), cancellationToken);
                }
                catch (Exception)
                {
                    // Should handle the exception here when the message cannot be deserialized or published on the Mediator
                }
                finally
                {
                    // Remove the lock on the message
                    await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                }
            }, new MessageHandlerOptions(LogMessageHandlerException)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });

            // Keep the BackgroundService running until the application shuts down
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            // Unsubscribe when the application is being stopped
            await subscriptionClient.UnregisterMessageHandlerAsync(TimeSpan.FromSeconds(1));
        }

        private static Task LogMessageHandlerException(ExceptionReceivedEventArgs arg) =>
            // Should handle the exception here when some error occurred reading the message from the ServiceBus
            Task.CompletedTask;
    }
}
