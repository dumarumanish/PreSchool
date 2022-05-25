using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities
{
    /// <summary>
    /// Codes for the entity.
    /// EntityName : name of the entity, eg: Staff, FamilyHead
    /// Prefix : Prefix of code (if any)
    /// Suffix : Suffix of code (if any)
    /// NumericLength : lenght of numeric value
    /// Eg : prefix : SF, Suffix: null, NumbericLength : 5 then Code : SF00001, SF01234,
    /// </summary>
    public class EntitiesCode
    {
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int NumericLength { get; set; }
    }
}
