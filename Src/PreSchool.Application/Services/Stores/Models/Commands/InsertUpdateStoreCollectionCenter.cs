using PreSchool.Application.Services.Addresses.Models.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreSchool.Application.Services.Stores.Models.Commands
{
    public class InsertUpdateStoreCollectionCenter
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int WarehouseId { get; set; }
        [Required]
        public string Name { get; set; }
        public InsertUpdateAddress Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public int DisplayOrder { get; set; }
       
    }
}
