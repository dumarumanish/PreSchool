using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertUpdateStoreAddress
    {
        /// <summary>
        /// Id of store address
        /// </summary>
        public int Id { get; set; }
        public InsertUpdateAddress Address { get; set; }
        public int DisplayOrder { get; set; }
    }
}
