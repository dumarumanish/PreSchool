using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;

namespace PreSchool.Application.Services.Notifications
{
    public class NotificationHub : Hub
    {
        private readonly IConnectionManager _connectionManager;
        private readonly ICurrentUserService _currentUserService;

        public NotificationHub(IConnectionManager connectionManager, ICurrentUserService currentUserService)
        {
            _connectionManager = connectionManager;
            _currentUserService = currentUserService;
        }

        public string GetConnectionId()
        {
            if (!_currentUserService.IsAuthenticated)
                return null;

            var appUserId = _currentUserService.AppUserId;
            _connectionManager.AddConnection(appUserId, Context.ConnectionId);
            return Context.ConnectionId;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = this.Context.GetHttpContext();

            // Remove from the online users
            if (httpContext.User.Identity.IsAuthenticated)
                _connectionManager.RemoveConnection(_currentUserService.AppUserId, Context.ConnectionId);


            return base.OnDisconnectedAsync(exception);
        }
    }
}
