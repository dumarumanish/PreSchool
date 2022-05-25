using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications
{
    public interface IConnectionManager
    {
        void AddConnection(int appUserId, string connectionId);
        void RemoveConnection(int appUserId, string connectionId);
        HashSet<string> GetConnections(int appUserId);
        IEnumerable<int> OnlineUsers { get; }
    }
}
