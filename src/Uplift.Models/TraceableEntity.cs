using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.Models
{
    public class TraceableEntity : BaseEntity
    {
        //[Required]
        public string CreateBy { get; set; }

        //[Required]
        public DateTime CreateAt { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
