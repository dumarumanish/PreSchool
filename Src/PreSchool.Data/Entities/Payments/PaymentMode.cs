using PreSchool.Data.Entities.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Payments
{

    public class PaymentMode : CommonTypeProperties
    {       
        public bool IsActive { get; set; }
        public int? ImageId { get; set; }
        public File Image { get; set; }

    }
}
