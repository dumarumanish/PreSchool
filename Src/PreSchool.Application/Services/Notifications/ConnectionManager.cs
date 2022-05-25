using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Notifications
{
    public class ConnectionManager : IConnectionManager
    {
        private static Dictionary<int, HashSet<string>> userMap = new Dictionary<int, HashSet<string>>();
        public IEnumerable<int> OnlineUsers
        {
            get
            {
                return userMap.Keys;
            }
        }

        public void AddConnection(int appUserId, string connectionId)
        {
            lock (userMap)
            {
                if (!userMap.ContainsKey(appUserId))
                {
                    userMap[appUserId] = new HashSet<string>();
                }
                userMap[appUserId].Add(connectionId);
            }
        }

        public HashSet<string> GetConnections(int appUserId)
        {
            var conn = new HashSet<string>();
            try
            {
                lock (userMap)
                {
                    conn = userMap[appUserId];
                }
            }
            catch
            {
                conn = null;
            }
            return conn;
        }

        public void RemoveConnection(int appUserId, string connectionId)
        {
            lock (userMap)
            {
                if (userMap.ContainsKey(appUserId))
                {
                    if (userMap[appUserId].Contains(connectionId))
                    {
                        userMap[appUserId].Remove(connectionId);
                    }

                    // Check if the user has any other connection, if not then remove from userMap
                    if (userMap[appUserId].Count == 0)
                        userMap.Remove(appUserId);
                }
            }
        }
    }
}
