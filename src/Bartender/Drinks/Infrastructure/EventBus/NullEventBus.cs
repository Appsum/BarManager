﻿using System.Threading.Tasks;

using Bartender.Api.Drinks.Application.EventBus;

namespace Bartender.Api.Drinks.Infrastructure.EventBus
{
    public class NullEventBus : IEventBus
    {
        public Task Publish(IMessage message) => Task.CompletedTask;
    }
}