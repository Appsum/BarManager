﻿using System;
using System.Text.Json;
using System.Threading.Tasks;

using Bartender.Drinks.Application.EventBus;

using Microsoft.Azure.ServiceBus;

namespace Bartender.Drinks.Infrastructure.EventBus
{
    public class AzureEventBus : IEventBus
    {
        private readonly ITopicClient _topicClient;

        public AzureEventBus(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }
        public async Task Publish(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            
            byte[] payload = JsonSerializer.SerializeToUtf8Bytes(message, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var serviceBusMessage = new Message(payload);
            await _topicClient.SendAsync(serviceBusMessage);
        }
    }
}
