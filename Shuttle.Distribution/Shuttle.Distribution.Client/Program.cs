﻿using System;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Distribution.Messages;

namespace Shuttle.Distribution.Client
{
	class Program
	{
		static void Main(string[] args)
		{
            var container = new DefaultComponentContainer();

            DefaultConfigurator.Configure(container);

            using (var bus = ServiceBus.Create(container).Start())
            {
                string userName;

				while (!string.IsNullOrEmpty(userName = Console.ReadLine()))
				{
					bus.Send(new RegisterMemberCommand
					{
						UserName = userName
					});
				}
			}
		}
	}
}
