using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BNS.Models
{
    public abstract class BaseEntity<TKey>
    {
        public TKey ID { get; set; }
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime? UpdateTime { get; set; }
        public bool IsRemoved { get; set; } = false;
        public DateTime? RemoveTime { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<long>
    {
    }
    
}
