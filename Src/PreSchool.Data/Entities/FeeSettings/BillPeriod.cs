using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.FeeSettings
{
    public class BillPeriod : CommonProperties
    {
        /// <summary>
        /// Gets or sets the month name
        /// </summary>
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UniqueCode { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<FeeGroup> FeeGroups { get; set; }



        public BillPeriod()
        {

            FeeGroups = new HashSet<FeeGroup>();
          
        }


    }
}
