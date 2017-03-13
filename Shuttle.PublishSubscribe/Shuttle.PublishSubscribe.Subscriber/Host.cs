﻿using System;
using Shuttle.Core.Host;
using Shuttle.Core.Infrastructure;
using Shuttle.Core.StructureMap;
using Shuttle.Esb;
using Shuttle.PublishSubscribe.Messages;
using StructureMap;

namespace Shuttle.PublishSubscribe.Subscriber
{
	public class Host : IHost, IDisposable
	{
		private IServiceBus _bus;

		public void Dispose()
		{
			_bus.Dispose();
		}

		public void Start()
		{
			var smRegistry = new Registry();
			var registry = new StructureMapComponentRegistry(smRegistry);

			ServiceBus.Register(registry);

			var resolver = new StructureMapComponentResolver(new Container(smRegistry));

			resolver.Resolve<ISubscriptionManager>().Subscribe<MemberRegisteredEvent>();

			_bus = ServiceBus.Create(resolver).Start();
		}
	}
}