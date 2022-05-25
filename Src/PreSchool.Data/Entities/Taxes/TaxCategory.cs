using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities.Taxes
{
    /// <summary>
    /// Tax on various type of product
    /// Eg: Books, Electronic & software, Downloadable products, Jewlry, Apparel
    /// </summary>
    public class TaxCategory : CommonProperties
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int DisplayOrder { get; set; }

    }
}
