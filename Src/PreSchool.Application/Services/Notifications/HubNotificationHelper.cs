using PreSchool.Application.Services.Notifications.Models.Dtos;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.Notifications
{
    public class HubNotificationHelper : IHubNotificationHelper
    {
        private readonly IHubContext<NotificationHub> _context;
        private readonly IConnectionManager _connectionManager;

        public HubNotificationHelper(IHubContext<NotificationHub> context, IConnectionManager connectionManager)
        {
            _context = context;
            _connectionManager = connectionManager;
        }
        public IEnumerable<int> GetOnlineUsers()
        {
            return _connectionManager.OnlineUsers;
        }

        public async Task<Task> SendNotificationParallel(int appUserId, string message)
        {
            var connections = _connectionManager.GetConnections(appUserId);
            try
            {
                if (connections != null && connections.Count > 0)
                {
                    foreach (var conn in connections)
                    {
                        try
                        {
                            await _context.Clients.Clients(conn).SendAsync("NewNotification", message);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            return Task.CompletedTask;
                            //throw new Exception("Error : No Connections found");
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Task.CompletedTask;

            }
        }

        public void SendNotificationToAll(string message)
        {
            _context.Clients.All.SendAsync("NewNotification", message);
        }

        public async Task<Task> SendRadiusCustomerSyncNotificationParallel(int appUserId, RadiusCustomerSyncNotificationDto message)
        {
            var connections = _connectionManager.GetConnections(appUserId);
            try
            {
                if (connections != null && connections.Count > 0)
                {
                    foreach (var conn in connections)
                    {
                        try
                        {
                            await _context.Clients.Clients(conn).SendAsync("RadiusCustomerSyncNotification", JsonConvert.SerializeObject(message));
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                            return Task.CompletedTask;
                            //throw new Exception("Error : No Connections found");
                        }
                    }
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return Task.CompletedTask;

                //throw ex;
            }
        }


    }
}
