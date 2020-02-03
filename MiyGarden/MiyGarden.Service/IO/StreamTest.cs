using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.IO
{
    public class StreamTest
    {
        public void CreateOrWrite()
        {
            using (var stream = new FileStream(@"D:\FileStream\hello.txt", FileMode.Append))
            {
                var text = Encoding.Unicode.GetBytes("\r\n" + DateTime.Now);
                stream.Write(text, 0, text.Count());
                stream.Close();
            }
        }
    }
}
