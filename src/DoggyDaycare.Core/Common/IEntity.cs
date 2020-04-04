using System;
using System.Collections.Generic;
using System.Text;

namespace DoggyDaycare.Core.Common
{
    public interface IEntity
    {
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedUtc { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedUtc { get; set; }
    }
}
