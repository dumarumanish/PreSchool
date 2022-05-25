using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Data.Entities
{
    public class CommonProperties : BaseEntity, IBaseEntity
    {
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }

    public interface IBaseEntity
    {
        bool IsDeleted { get; set; }
    }
}
