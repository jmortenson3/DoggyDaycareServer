using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedUtc { get; set; }
    }
}
