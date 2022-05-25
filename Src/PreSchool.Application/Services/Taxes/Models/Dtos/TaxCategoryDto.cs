using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.Taxes.Models.Dtos
{
    public class TaxCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int DisplayOrder { get; set; }
    }
}
