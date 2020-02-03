using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Linq
{
    public delegate void OnInit(object sender, EventArgs e);

    public class Delegate
    {
        public event OnInit InitEvent;

        public void Init()
        {
            InitEvent?.Invoke(this, null);
        }
    }
}
