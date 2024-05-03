using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// 測試檔案類型之hex簽名
        /// </summary>
        public void StartFileStream()
        {
            string[] paths = { @"C:\Users\kamis\Pictures\images.jpg", @"C:\Users\kamis\Pictures\images.pdf", @"C:\Users\kamis\Pictures\images.xlsx" };
            //string mimeType = MimeMapping.GetMimeMapping(paths[2]);

            List<string> result = new List<string>();
            foreach (var path in paths)
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    int hexIn;
                    var hex = new StringBuilder();

                    while ((hexIn = fs.ReadByte()) != -1)
                    {
                        hex.Append(string.Format("{0:X2}", hexIn));
                    }

                    result.Add(hex.ToString());
                }
            }
        }
    }
}
