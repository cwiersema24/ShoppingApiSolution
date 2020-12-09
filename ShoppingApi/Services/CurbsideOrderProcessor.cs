using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingApi.Data;
using ShoppingApi.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingApi.Services
{
    public class CurbsideOrderProcessor : BackgroundService
    {
        private readonly ILogger<CurbsideOrderProcessor> _logger;
        private readonly CurbsideChannel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<CurbsideOrdersHub> _hub;

        public CurbsideOrderProcessor(ILogger<CurbsideOrderProcessor> logger, CurbsideChannel channel, IServiceProvider serviceProvider, IHubContext<CurbsideOrdersHub> hub)
        {
            _logger = logger;
            _channel = channel;
            _serviceProvider = serviceProvider;
            _hub = hub;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach(var order in _channel.ReadAllAsync())
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ShoppingDataContext>();

                var saveOrder = await context.CurbSide.SingleOrDefaultAsync(o => o.Id == order.ReservationId);
                if(saveOrder == null)
                {
                    continue;
                }
                else
                {
                    var numberOfItems = saveOrder.Items.Split(',').Count();
                    for (var t = 0; t<numberOfItems; t++)
                    {
                        await Task.Delay(1000);
                        if(order.ConnectionId != null)
                        {
                            await _hub.Clients.Client(order.ConnectionId).SendAsync("ItemProcessed",
                                new
                                {
                                    message = $"Processed Item {t + 1} of your order",
                                    orderId= order.ReservationId
                                });
                        }
                    }
                    saveOrder.Status = CurbSideOrderStatus.Approved;
                    saveOrder.PickupDate = DateTime.Now.AddHours(numberOfItems);
                    if (order.ConnectionId != null)
                    {
                        await _hub.Clients.Client(order.ConnectionId).SendAsync("OrderProcessed", saveOrder);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
