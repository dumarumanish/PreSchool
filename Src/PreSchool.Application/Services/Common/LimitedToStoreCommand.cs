using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services
{
    public class LimitedToStoresCommand
    {
        /// <summary>
        /// Whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// If entity is limited to certain stores then their store's Id
        /// </summary>
        public List<int> AddStores { get; set; }

        /// <summary>
        /// If entity was already added but need to be removed
        /// </summary>
        public List<int> RemoveStores { get; set; }

        public LimitedToStoresCommand()
        {
            AddStores = new List<int>();
            RemoveStores = new List<int>();
        }
    }
}
