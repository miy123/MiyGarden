using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Models.Models
{
    public class BaseField
    {
        public DateTime CreateDate { set; get; } = DateTime.Now;
        public DateTime UpdateDate { set; get; } = DateTime.Now;
    }
}
