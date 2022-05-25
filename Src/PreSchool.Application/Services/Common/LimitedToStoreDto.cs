using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services
{
    public class LimitedToStoresDto
    {
        /// <summary>
        /// Whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }
        public List<int> Stores { get; set; }

        public LimitedToStoresDto()
        {
            Stores = new List<int>();
        }
    }
}
