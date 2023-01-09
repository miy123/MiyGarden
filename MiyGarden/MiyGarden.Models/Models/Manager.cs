using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Models.Models
{
    internal class Manager : Employee
    {
        public string Name { get; set; }

        public override string GetProgressReport() => "Manager overrides GetProgressReport";
    }
}
